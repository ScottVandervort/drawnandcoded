<%@ Page Title="Simple AJAX Echo Service With Response Wrapper" Language="C#" AutoEventWireup="true" CodeBehind="EchoPage.aspx.cs" Inherits="SimpleAJAXEchoService.EchoPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Simple AJAX Echo Service With Response Wrapper</title>
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

		/*  A "blank" Message exposed by the server-side code that will be used to submit a Message to the Echo Service. 
			Now using the .GetJsonString() extension method to the object class.
		*/		
		var jsonMessage = <%= (new SimpleAJAXEchoService.Message()).GetJsonString() %>

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

				Notice that "dataType" has been commented out. The data conversion is now handled by the "dataFilter". The dataFilter automatically parses the
				Echo Service's response and removes the (nasty) .d encapsulation enforced by ASP.NET 3.5 and above.
				http://encosia.com/2009/06/29/never-worry-about-asp-net-ajaxs-d-again/

				The "success" function is (obviosuly) called upon a successful transaction. The data parameter is the data returned from the Echo Service.
				Notice that the .d encapsulation no longer has to be handled thanks to the dataFilter.
			*/
			$.ajax({
				type: "POST",
				url: "EchoService.svc/Echo",
				data: '{ "message" : ' + JSON.stringify(jsonMessage) + '}',	
				/* dataType: "json",  */
				contentType: "application/json; charset=utf-8",      
				dataFilter: function(data) {
					
					/*  Since we are explicitly filtering data now we can't rely upon JQuery to marshall the types conversions. We need to parse the JSON string
						returned from the Echo Service into a JSON object. 
					*/
					data = JSON.parse(data);

					/*	Unwrap the data from the ASP.NET 3.5 .d wrapper (if necessary). */
					data = data.hasOwnProperty("d") ? data.d : data;

					/*  The Echo Service now returns an encapsulated data object (the ClientResponse) . We can check the IsSuccessful property to make sure no errors 
						were incurred while the Echo Service was processing the request. 
					*/						
					if (data.hasOwnProperty("IsSuccessful")) {

						if (data.IsSuccessful == true) {

							return data.Payload;	
						}
						else {
							
							var errorMessage = "Error";

							if (data.hasOwnProperty("ErrorMessage") && data.ErrorMessage !== null) {

								errorMessage = data.ErrorMessage;
							}

							throw errorMessage;
						
						}						
					}					

					return data;					
				},				       
				success: function(data, textStatus, httpRequest) {
									
					/* Upon a successful call to the Echo Service this function will be called. "data" is the data returned from the Echo Service call. */									
										
					/* data = data.hasOwnProperty('d') ? data.d :  data; */										

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



