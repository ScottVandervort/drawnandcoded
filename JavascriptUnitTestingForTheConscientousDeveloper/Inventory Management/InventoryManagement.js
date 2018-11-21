var ScottsJewels = ScottsJewels || {};
ScottsJewels.InventoryManagement = ScottsJewels.InventoryManagement || {};

ScottsJewels.InventoryManagement = (function () {

	$(document).ready(function () {
	
		var 	$selectInventoryCategory,
			$buttonShowAddInventory;
		
		$selectInventoryCategory = $("#selectInventoryCategory");			
		$buttonShowAddInventory = $("#buttonShowAddInventory");
	
		// Hook up event handlers.
	
		$selectInventoryCategory.bind ( "change", ScottsJewels.InventoryManagement.selectInventoryOnChange);		
		$buttonShowAddInventory.bind ( "click", ScottsJewels.InventoryManagement.showAddInventoryForm);
	
	});

    return {
			
		selectInventoryOnChange : function (event) {							
				
			// Get Category from selectInventoryCategory <select> and invoke getInventoryFromServer().													
				
			var inventoryCategory;

			inventoryCategory = $(event.target).val();
			
			if (inventoryCategory !== "") {
			
				ScottsJewels.InventoryManagement.getInventoryFromServer(inventoryCategory);			
			}			
		},

		getInventoryFromServer : function ( inventoryCategory ) {
				   
			// Make AJAX GET Request to Web Server for Category and invoke refreshInventoryTable() with retreived Data.		
			
			$.ajax({
				type: "GET",            
				url : "http://ScottsJewels/InventoryManagement/GetInventory",
				data: { inventoryCategory : inventoryCategory },
				success: ScottsJewels.InventoryManagement.refreshInventoryTable        				
			});					
		},
		
		refreshInventoryTable : function ( jsonInventoryData ) {
		
			// Refresh <table> with Inventory Data.		
		
			var 	$tableInventoryBody,
				html;

			$tableInventoryBody = $("#tableInventory tbody");			
			html = "";
		
			$(jsonInventoryData).each( function ( index, value ) {
			
				html += 	"<tr>";
				html += 		"<td class='sku'>" + value.SKU + "</td>";
				html += 		"<td class='name'>" + value.Name + "</td>";
				html += 		"<td class='quanity'>" + value.Quantity + "</td>";
				html +=	"</tr>";			
			});
			
			$tableInventoryBody
				.empty()
				.append(html);			
		},
		
		showAddInventoryForm : function () {
		
			// Display the formAddInventory <form> in a JQuery UI Dialog.		
		
			var $formAddInventory;
			
			$formAddInventory = $("#formAddInventory");
						
			$formAddInventory[0].reset();                						
			
			$formAddInventory.dialog({ 
				buttons: [{	text: "Ok", 
							click: ScottsJewels.InventoryManagement.addNewInventoryClick 
						}]});

		},
		
		addNewInventoryClick : function () {
		
			// Retreive data from formAddInventory <form>, invoke addNewInventory(), and hide JQuery UI Dialog.
		
			var 	$formAddInventory,
				formData;
			
			$formAddInventory = $("#formAddInventory");		
			
			formData = $formAddInventory.serialize();
									
			$formAddInventory.dialog("close");
			
			ScottsJewels.InventoryManagement.addNewInventory( formData );				
		},
		
		addNewInventory : function ( formData ) {
		
			// Make AJAX POST to Web Server with <form> data.		
		
			$.ajax({
				type			: "POST",   
				contentType	: "application/x-www-form-urlencoded",				
				url 			: "http://ScottsJewels/InventoryManagement/AddInventory",
				data			: formData
			});									
		}
	};
})();
				