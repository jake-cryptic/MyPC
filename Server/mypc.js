if (!window.jQuery){
	// Check for jquery
	var el = document.createElement("script");
	el.src = "jquery.js";
	el.type = "text/javascript";
	document.head.appendChild(el);
}

$(document).ready(function(){
	/*
	 *	When the document loads, check if MyPC client is running.
	 *	If it is, then get the JSON data from the server.
	*/
	$.ajax({
		url:"http://localhost:9091/results/",
		crossDomain: true,
		type:"GET",
		dataType:"json",
		beforeSend: function() {
			$("#loading_txt").html("Please wait");
		},
		success: function(data) {
			$("#loading_txt").html("Generating Report");
			ShowUserResult(data);
		},
		error: function(e) {
			ShowHomePage();
		}
	});
});

var CloseServer = function(){
	// What is this, you say?!
	// Secret ;)
	// ...
	// (actually you could probably guess)
	$.ajax({
		url:"http://localhost:9091/close",
		crossDomain: true,
		type:"GET",
		dataType:"text",
		success: function(data) {
			alert("Connection to server closed");
		},
		error: function(e) {
			alert("Connection to server closed");
		}
	});
};
var ShowHomePage = function(){
	document.title = "MyPC | Homepage";
	
	// Activate the homepage links
	$("#dl_mypc").click(function(){
		$("body").fadeOut(250);
		window.location.href = "https://github.com/jake-cryptic/MyPC/releases/";
	});
	$("#np_git").click(function(){
		$("body").fadeOut(250);
		window.location.href = "https://github.com/jake-cryptic/MyPC";
	});
	$("#goto_http").click(function(){
		window.location.href = "http://" + window.location.hostname + window.location.pathname;
	});
	$("#ad").click(function(){
		window.location.href = "https://absolutedouble.co.uk/";
	});
	
	// Show the home page
	$("#loader").fadeOut(200);
	$("#home").delay(200).fadeIn(200);
	
	if (window.location.protocol === "https:"){
		// Due to the MyPC client running over HTTP, HTTPS requests to it will be blocked
		// To circumvent this we ask the user to switch to HTTP... :/
		$("#https_warning").delay(550).slideDown(500);
		setTimeout(function(){
			$("#https_warning").addClass("warn");
		},1000);
	}
};
var ShowUserResult = function(data){
	console.log(data);
	CreateMyPC(data);
	
	$(".sect_title").click(function(){
		if ($(this).is(":visible")){
			$(this).slideUp(500);
		} else {
			$(this).slideDown(500);
		}
	});
	
	$("#loader").fadeOut(200);
	$("#mypc").delay(200).fadeIn(200);
};
var CreateMyPC = function(d){
	/*
	 *	This (rather large) function will interpret the JSON data from MyPC client.
	 *	There is a more efficient way of doing this.. which I will implement soon...
	*/
	
	document.title = "MyPC | " + d.CPU[0][7];
	
	// Set PC name
	$("#pc_nm").html(d.CPU[0][7]);
	$("#pc_rg").html("Report Generated: " + d.Generated);
	
	// System Section
	$("#sys").append(
		$("<tr/>",{}).append(
			$("<td/>", {}).text("Operating System"),
			$("<td/>", {}).text(d.INF[0] + " " + d.INF[3])
		),
		$("<tr/>",{}).append(
			$("<td/>", {}).text("Install Date"),
			$("<td/>", {}).text(d.INF[2])
		),
		$("<tr/>",{}).append(
			$("<td/>", {}).text("Country Code"),
			$("<td/>", {}).text(d.INF[1])
		)
	);
	
	// CPU Section
	for (var i = 0;i<d.CPU.length;i++){
		var clss = "dark";
		if (i % 2 == 0) clss = "light";
		$("#cpu").append(
			$("<tr/>",{
				class:clss
			}).append(
				$("<td/>", {}).text("Manufacturer"),
				$("<td/>", {}).text(d.CPU[i][2])
			),
			$("<tr/>",{
				class:clss
			}).append(
				$("<td/>", {}).text("CPU Name"),
				$("<td/>", {}).text(d.CPU[i][4])
			),
			$("<tr/>",{
				class:clss
			}).append(
				$("<td/>", {}).text("Clock Speed"),
				$("<td/>", {}).text(d.CPU[i][1])
			)
		);
	}
	
	// Motherboard Section
	$("#bse").append(
		$("<tr/>",{}).append(
			$("<td/>", {}).text("Manufacturer"),
			$("<td/>", {}).text(d.BSE[1])
		),
		$("<tr/>",{}).append(
			$("<td/>", {}).text("Board Name"),
			$("<td/>", {}).text(d.BSE[2])
		),
		$("<tr/>",{}).append(
			$("<td/>", {}).text("Hot Swappable"),
			$("<td/>", {}).text(d.BSE[0])
		)
	);
	
	// RAM Section
	$("#ram").append(
		$("<tr/>",{
			class:"dark"
		}).append(
			$("<td/>", {}).text("Sticks"),
			$("<td/>", {}).text(d.RAM.length)
		)
	);
	for (var i = 0;i<d.RAM.length;i++){
		var clss = "dark";
		if (i % 2 == 0) clss = "light";
		$("#ram").append(
			$("<tr/>",{
				class:clss
			}).append(
				$("<td/>", {}).text("Manufacturer"),
				$("<td/>", {}).text(d.RAM[i][0])
			),
			$("<tr/>",{
				class:clss
			}).append(
				$("<td/>", {}).text("Capacity"),
				$("<td/>", {}).text(d.RAM[i][1])
			)
		);
	}
	
	// GPU Section
	for (var i = 0;i<d.GPU.length;i++){
		var clss = "dark";
		if (i % 2 == 0) clss = "light";
		$("#gpu").append(
			$("<tr/>",{
				class:clss
			}).append(
				$("<td/>", {}).text("Manufacturer"),
				$("<td/>", {}).text(d.GPU[i][0])
			),
			$("<tr/>",{
				class:clss
			}).append(
				$("<td/>", {}).text("GPU"),
				$("<td/>", {}).text(d.GPU[i][3])
			),
			$("<tr/>",{
				class:clss
			}).append(
				$("<td/>", {}).text("GPU ID"),
				$("<td/>", {}).text(d.GPU[i][2])
			),
			$("<tr/>",{
				class:clss
			}).append(
				$("<td/>", {}).text("GPU RAM"),
				$("<td/>", {}).text(d.GPU[i][1])
			)
		);
	}
	
	// Storage Section
	for (var i = 0;i<d.HDD.length;i++){
		var clss = "dark";
		if (i % 2 == 0) clss = "light";
		$("#hdd").append(
			$("<tr/>",{
				class:clss
			}).append(
				$("<td/>", {}).text("Drive Name"),
				$("<td/>", {}).text(d.HDD[i][6] + " (" + d.HDD[i][7] + ")")
			),
			$("<tr/>",{
				class:clss
			}).append(
				$("<td/>", {}).text("File System"),
				$("<td/>", {}).text(d.HDD[i][3])
			),
			$("<tr/>",{
				class:clss
			}).append(
				$("<td/>", {}).text("Free/Total Space"),
				$("<td/>", {}).text(d.HDD[i][4] + " of " + d.HDD[i][5])
			),
			$("<tr/>",{
				class:clss
			}).append(
				$("<td/>", {}).text("Description"),
				$("<td/>", {}).text(d.HDD[i][1])
			)
		);
	}
	
	// Network Cards Section
	for (var i = 0;i<d.NIC.length;i++){
		if (d.NIC[i].length == 8){
			var clss = "dark";
			if (i % 2 == 0) clss = "light";
			$("#nic").append(
				$("<tr/>",{
					class:clss
				}).append(
					$("<td/>", {}).text("Manufacturer"),
					$("<td/>", {}).text(d.NIC[i][3])
				),
				$("<tr/>",{
					class:clss
				}).append(
					$("<td/>", {}).text("Name"),
					$("<td/>", {}).text(d.NIC[i][5])
				),
				$("<tr/>",{
					class:clss
				}).append(
					$("<td/>", {}).text("MAC Address"),
					$("<td/>", {}).text(d.NIC[i][2])
				),
				$("<tr/>",{
					class:clss
				}).append(
					$("<td/>", {}).text("Description"),
					$("<td/>", {}).text(d.NIC[i][0])
				)
			);
		}
	}
	
	// User Account Section
	for (var i = 0;i<d.USR.length;i++){
		var clss = "dark";
		if (i % 2 == 0) clss = "light";
		$("#usr").append(
			$("<tr/>",{
				class:clss
			}).append(
				$("<td/>", {}).text("Username"),
				$("<td/>", {}).text(d.USR[i][0])
			),
			$("<tr/>",{
				class:clss
			}).append(
				$("<td/>", {}).text("Description"),
				$("<td/>", {}).text(d.USR[i][1])
			),
			$("<tr/>",{
				class:clss
			}).append(
				$("<td/>", {}).text("Disabled"),
				$("<td/>", {}).text(d.USR[i][2])
			),
			$("<tr/>",{
				class:clss
			}).append(
				$("<td/>", {}).text("Name"),
				$("<td/>", {}).text(d.USR[i][3])
			),
			$("<tr/>",{
				class:clss
			}).append(
				$("<td/>", {}).text("Password Required"),
				$("<td/>", {}).text(d.USR[i][4])
			)
		);
	}
};