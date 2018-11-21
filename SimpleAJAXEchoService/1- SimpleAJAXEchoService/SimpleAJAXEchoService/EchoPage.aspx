<%@ Page Title="Simple AJAX Echo Service" Language="C#" AutoEventWireup="true" CodeBehind="EchoPage.aspx.cs" Inherits="SimpleAJAXEchoService.EchoPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Simple AJAX Echo Service</title>
	<style type="text/css">
	
		.error  
		{			
			color: Red;		
		}
		form
		{
			max-width:300px;
		}
	
	</style>	
	<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js"></script>
	<script type="text/javascript" src="https://github.com/douglascrockford/JSON-js/raw/master/json2.js"></script>
	<script type="text/javascript">

		/* A "blank" Message exposed by the server-side code that will be used to submit a Message to the Echo Service. */
		var jsonMessage = <%= jsonMessage %>;

		/* Submits the specified message to the Echo Service. */
		function submitMessage (message) {

			jsonMessage.Text = message;
			
			$("#responseMessage").removeClass("error");
			$("#responseMessage").html("");				

			/*	Use JQuery's ajax() call to invoke the Echo Service. 
				http://api.jquery.com/jQuery.ajax/

				Be very careful when specifying your "data". The data itself is in the form of a JSON (name/value pair) object where the name corresponds 
				to the parameter name on the Echo Service's method. The value is your actual data - serialized using the JSON.Stringify() function. Also, 
				take special notice of the parenthesis. I've had problems before with single versus double-quotes. 

				The "dataType : json" instructs JQuery to expect a JSON result from the Echo Service. JQuery will implicitly convert the Echo Service's 
				response.

				The "success" function is (obviosuly) called upon a successful transaction. The data parameter is the data returned from the Echo Service.
			*/
			$.ajax({
				type: "POST",
				url: "EchoService.svc/Echo",
				data: '{ "message" : ' + JSON.stringify(jsonMessage) + '}',
				dataType: "json",											
				contentType: "application/json; charset=utf-8",             
				success: function(data, textStatus, httpRequest) {
									
					/* Upon a successful call to the Echo Service this function will be called. "data" is the data returned from the Echo Service call. */																	
										
					/*	Since ASP.NET 3.5 Microsoft's WCF Service encapsulates all JSON responses in a "d" name-value pair for security reasons. 
						http://www.asp.net/ajaxlibrary/Using%20JSON%20Syntax%20with%20Ajax.ashx
						http://haacked.com/archive/2009/06/25/json-hijacking.aspx
					*/
					data = data.hasOwnProperty('d') ? data.d :  data;										
                					
					$("#responseMessage").html(data.Text);
				},
				error: function(httpRequest, status, errorThrown) {

					/* Upon failed call to the Echo Service this function will be called. */

					$("#responseMessage").addClass("error");
					$("#responseMessage").html("There was a problem calling the Echo Service. Error : " + errorThrown + "!");							
				}
			});
		}
	
	</script>
</head>
<body>
	<form>
		<fieldset>
			<legend>Echo Message</legend>
			<input type="text" id="submitMessage" />		
		</fieldset>		
	</form>
	<input type="button" id="submitButton" value="Submit" onclick="submitMessage($('#submitMessage').val());"/>
	<br />Response : <span id="responseMessage"></span>	
</body>
</html>



