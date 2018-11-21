QUnit.module( "ScottsJewels.InventoryManagement",
              { 
				setup     : function() {
													
				},
				teardown  : function() {

				} 
              });
       
QUnit.test( "selectInventoryOnChange()", 1, function() {

	// Test : Verify that getInventoryaFromServer() is called with the selected inventory as a parameter.
	
	var 	$event,
		$selectInventoryCategory,
		$optionFood,
		original_getInventoryFromServer;
			
	// Set the Inventory Category <select> with "Food" <option>.
	$selectInventoryCategory = $("#selectInventoryCategory");	
	$selectInventoryCategory.val("Food");
	
	// Create a "change" event.
	$event = $.Event("change");		
	$event.target = $selectInventoryCategory;		

	try {
	
		// Save a reference to getInventoryFromServer() so that it can be restored after the test has completed.
		original_getInventoryFromServer = ScottsJewels.InventoryManagement.getInventoryFromServer;	
		
		// Now, assign a new function to getInventoryFromServer().
		ScottsJewels.InventoryManagement.getInventoryFromServer = function ( inventoryCategory ) {
		
			// Verify that getInventoryFromServer is invoked with the "Food" <option>.
			equal( inventoryCategory, "Food" ); 				
		}
		
		// Invoke selectInventoryOnChange() and send in the "change" event.
		// selectInventoryOnChange() should invoke getInventoryFromServer which was (temporarily) redefined above.
		ScottsJewels.InventoryManagement.selectInventoryOnChange ($event);
	}
	finally {
	
		// Restore getInventoryFromServer().
		ScottsJewels.InventoryManagement.getInventoryFromServer = original_getInventoryFromServer;
	}
	                       
});       

QUnit.test( "getInventoryFromServer()", 3, function() {

	// Test : Verify the AJAX HTTP GET Request is correct.
	// Test : Verify that refreshInventoryTable() is called.

	var 	original_ajax;
	
	try {
		// Save a reference to JQuery's ajax() so that it can be restored after the test is complete.
		original_ajax = $.ajax;		
		
		// Now, assign a new funtion to JQuery's ajax().
		$.ajax = function (request) {
		
			// Verify that "Food" was supplied as the ajax "data" parameter.
			equal(request.data.inventoryCategory, "Food");
			
			// Verify that the ajax is a GET.		
			equal(request.type, "GET");				
			
			// Verify that refreshInventoryTable() will be invoked if ajax is successful.
			equal(request.success, ScottsJewels.InventoryManagement.refreshInventoryTable);
		}
		
		// Invoke JQuery's ajax(). This should invoke the (temporarily) re-defined ajax() above.
		ScottsJewels.InventoryManagement.getInventoryFromServer ( "Food" );	
	}
	finally {
	
		// Restore JQuery's ajax().
		$.ajax = original_ajax;			
	}
	                     
});    

QUnit.test( "refreshInventoryTable()", 9, function() {

	// Test : Verify the <table> is correctly refreshed in DOM.

	var 	jsonInventoryData,
		$tableInventoryBody;
	
	// Create a JSON object of data to bind to the <table>.
	jsonInventoryData = [{ 	SKU 		: "1234",
						Name 	: "Item 1",
						Quantity	: 1},
					{ 	SKU 		: "1235",
						Name 	: "Item 2",
						Quantity	: 2},						
					{ 	SKU 		: "1236",
						Name 	: "Item 3",
						Quantity	: 3}];
			
	// Get the <table>'s <tbody>.
	$tableInventoryBody = $("#tableInventory tbody");
	
	// Invoke refreshInventoryTable() with the JSON data.
	ScottsJewels.InventoryManagement.refreshInventoryTable (jsonInventoryData);
		
	// Verify that the <table>'s <tbody> was populated properly.
	equal( $tableInventoryBody.children("tr:nth-child(1)").children("td.sku").html(), 1234 );                      
	equal( $tableInventoryBody.children("tr:nth-child(1)").children("td.name").html(), "Item 1" );                      
	equal( $tableInventoryBody.children("tr:nth-child(1)").children("td.quanity").html(), 1 );                                            	
	
	equal( $tableInventoryBody.children("tr:nth-child(2)").children("td.sku").html(), 1235 );                      
	equal( $tableInventoryBody.children("tr:nth-child(2)").children("td.name").html(), "Item 2" );                      
	equal( $tableInventoryBody.children("tr:nth-child(2)").children("td.quanity").html(), 2 );                                            		
	
	equal( $tableInventoryBody.children("tr:nth-child(3)").children("td.sku").html(), 1236 );                      
	equal( $tableInventoryBody.children("tr:nth-child(3)").children("td.name").html(), "Item 3" );                      
	equal( $tableInventoryBody.children("tr:nth-child(3)").children("td.quanity").html(), 3 );                                            				                      
});    

QUnit.test( "showAddInventoryForm()", 4, function() {

	// Test : Verify that the appropriate <form> is displayed.
	// Test : Verify that the <form> fields are empty.
	
	var $formAddInventory;
	
	// Get the <form> that will be displayed in a JQuery dialog.
	$formAddInventory = $("#formAddInventory");
	
	try {
	
		// Invoke showAddInventoryForm() to display the <form>.
		ScottsJewels.InventoryManagement.showAddInventoryForm ();

		// Verify that the <form> is visible on the JQuery dialog.
		equal( $formAddInventory.dialog( "isOpen" ), true );      

		// Verify that the <form>'s fields are empty.
		equal( $formAddInventory.find("#inputSKU").val(), "" );                      
		equal( $formAddInventory.find("#inputName").val(), "" );                      
		equal( $formAddInventory.find("#inputQuantity").val(), "" );                      			
	}	
	finally {
	
		// Explictly close the JQuery dialog - otherwise QUnit will leave it as an artifact.
		$formAddInventory.dialog("close");	
	}
	                       
});    

QUnit.test( "addNewInventoryClick()", 2, function() {

	// Test : Verify that the <form> is hidden.
	// Test : Verify that addNewInventory() is called with the appropriate data from the <form>.
	
	var 	original_addNewInventory,
		$formAddInventory;
	
	// Get the <form> that is displayed in the JQuery dialog.	
	$formAddInventory = $("#formAddInventory");	
			
	// Set the fields on the <form>.
	$formAddInventory.find("#inputSKU").val("1234");
	$formAddInventory.find("#inputName").val("Hot Dogs");
	$formAddInventory.find("#inputQuantity").val(3);
		
	// Save a reference to addNewInventory() so that it can be restored after the test is complete.
	original_addNewInventory = ScottsJewels.InventoryManagement.addNewInventory;
		
	try {
	
		// Now assign a new function to addNewInventory().
		ScottsJewels.InventoryManagement.addNewInventory = function ( formData ) {
					
			// Verify the te correct <form> data is supplied.
			equal( formData, "sku=1234&name=Hot+Dogs&quantity=3" ); 			
			
			// Verify the JQuery dialog hosting the <form> is closed.
			notEqual( $formAddInventory.dialog( "isOpen" ), true ); 				
		};
		
		// Invoke addNewInventoryClick(). This should invoke the (temporarily) re-defined addNewInventoryClick() above.
		ScottsJewels.InventoryManagement.addNewInventoryClick();						
	}
	finally {
	
		// Restore addNewInventoryClick().
		ScottsJewels.InventoryManagement.addNewInventory = original_addNewInventory;	
	}			
});

QUnit.test( "addNewInventory()", 2, function() {

	// Test : Verify the AJAX HTTP POST Request is correct.	

	ScottsJewels.InventoryManagement.addNewInventory ();

	var 	original_ajax,
		formData;
		
	// Create the <form> data.
	formData = "sku=1234&name=Hot+Dogs&quantity=3";
	
	try {
		// Save a reference to JQuery's ajax() so that it can be restored after the test is complete.
		original_ajax = $.ajax;		
		
		// Now, assign a new funtion to JQuery's ajax().
		$.ajax = function (request) {
		
			// Verify that the <form> data was supplied as the ajax "data" parameter.
			equal(request.data, "sku=1234&name=Hot+Dogs&quantity=3");
			
			// Verify that the ajax is a POST.
			equal(request.type, "POST");			
		}
		
		// Invoke JQuery's ajax(). This should invoke the (temporarily) re-defined ajax() above.
		ScottsJewels.InventoryManagement.addNewInventory ( formData );	
	}
	finally {
	
		// Restore getInventoryFromServer().
		$.ajax = original_ajax;			
	}
	                       
});    
