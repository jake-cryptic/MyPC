if (!window.jQuery) document.write("<script src='jquery.js'></script>");

$(document).ready(function(){
	$.ajax({
		url:"http://localhost:9091/results/",
        crossDomain: true,
		type:"GET",
		dataType:"json",
		success: function(data) {
			ShowUserResult(data);
		},
		error: function(e) {
			ShowHomePage();
		}
	});
});

var CloseServer = function(){
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
	$("#loader").fadeOut(200);
	$("#home").delay(200).fadeIn(200);
};
var ShowUserResult = function(data){
	console.log(data);
	CreateMyPC(data);
	$("#loader").fadeOut(200);
	$("#mypc").delay(200).fadeIn(200);
};
var CreateMyPC = function(d){
	document.title = "MyPC | " + d.CPU[7];
	
	// Set PC name
	$("#pc_nm").html(d.CPU[7]);
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
	$("#cpu").append(
		$("<tr/>",{}).append(
			$("<td/>", {}).text("Manufacturer"),
			$("<td/>", {}).text(d.CPU[2])
		),
		$("<tr/>",{}).append(
			$("<td/>", {}).text("CPU Name"),
			$("<td/>", {}).text(d.CPU[4])
		),
		$("<tr/>",{}).append(
			$("<td/>", {}).text("Clock Speed"),
			$("<td/>", {}).text(d.CPU[1])
		)
	);
	
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
		$("<tr/>",{}).append(
			$("<td/>", {}).text("Manufacturer"),
			$("<td/>", {}).text(d.RAM[0])
		),
		$("<tr/>",{}).append(
			$("<td/>", {}).text("Capacity"),
			$("<td/>", {}).text(d.RAM[1])
		)
	);
	
	// GPU Section
	$("#gpu").append(
		$("<tr/>",{}).append(
			$("<td/>", {}).text("Manufacturer"),
			$("<td/>", {}).text(d.GPU[0])
		),
		$("<tr/>",{}).append(
			$("<td/>", {}).text("GPU"),
			$("<td/>", {}).text(d.GPU[3])
		),
		$("<tr/>",{}).append(
			$("<td/>", {}).text("GPU ID"),
			$("<td/>", {}).text(d.GPU[2])
		),
		$("<tr/>",{}).append(
			$("<td/>", {}).text("GPU RAM"),
			$("<td/>", {}).text(d.GPU[1])
		)
	);
	
	// User Account Section
	$("#usr").append(
		$("<tr/>",{}).append(
			$("<td/>", {}).text("Username"),
			$("<td/>", {}).text(d.USR[0])
		),
		$("<tr/>",{}).append(
			$("<td/>", {}).text("Description"),
			$("<td/>", {}).text(d.USR[1])
		),
		$("<tr/>",{}).append(
			$("<td/>", {}).text("Disabled"),
			$("<td/>", {}).text(d.USR[2])
		),
		$("<tr/>",{}).append(
			$("<td/>", {}).text("Name"),
			$("<td/>", {}).text(d.USR[3])
		),
		$("<tr/>",{}).append(
			$("<td/>", {}).text("Password Required"),
			$("<td/>", {}).text(d.USR[4])
		)
	);
};