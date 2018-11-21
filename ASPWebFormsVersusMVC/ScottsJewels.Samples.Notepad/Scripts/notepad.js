$(document).ready(function () {
												
	$("#addNoteForm").link(jsonNote);

    $("#addNoteForm").submit(function (event) {

    	// Stop form from submitting normally.		
    	event.preventDefault();
										
    	var $form = $(this);
		var $message = $("#message");
		var url	= $form.attr('action');

		$('#addNoteForm').unlink(jsonNote);
											
    	/*	http://lozanotek.com/blog/archive/2010/04/16/posting_json_data_to_mvc_controllers.aspx
			
			When performing a POST to a web server the request typically has a content type and a payload.

			ASP.NET MVC relies on the DefaultModelBinder [http://msdn.microsoft.com/en-us/library/system.web.mvc.defaultmodelbinder.aspx] to map 
			the POST's payload to the parameters on a Controller. For example, in a form POST the content type is "application/x-www-form-urlencoded"
			and the payload is a name/value pair. The DefaultModelBinder will map the Controller's parameters to the name/value pairs by name. 

			The DefaultModelBinder does a great job mapping everything but JSON. JSON [link] has been all the rage lately because it is easily 
			intergrated with client-side JavaScript and has a very small footprint. Unfortunately, a POST with a content type of 
			"application/json; charset=utf-8" and a JSON payload will not be parsed by MVC's DefaultModelBinder.

			So what is one to do? JQuery to the rescue. The JQuery ajax() function is smart enough to convert a JSON payload to the appropriate 
			content type. All you have to do is specify "application/x-www-form-urlencoded" for the contentType parameter and JQuery 
			will massage the payload's data into a name/value pair compatible with a form POST. Pretty neat, huh?

			On the flip side MVC does allow you to return JSON from a Controller using the JsonResult ActionResult. Inconsistent, huh? 
			Here is a list of what an MVC Controller can return [http://msdn.microsoft.com/en-us/library/system.web.mvc.actionresult.aspx].
					 
			The contentType specifies that the data is going to be used for a form POST to the server. JQuery is smart enough to convert the data
			payload from JSON into a name/value pair that is compatible with a POST.
    	*/

    	$.ajax({
    		type: 'POST',
    		contentType: "application/x-www-form-urlencoded",
    		data: jsonNote,
    		url: url,
    		success:
					
				function (data) {

					$form.link(jsonNote);

					if (data.IsSuccessful == true) {
								
						$message.text("Note added successfully!");								

						jsonNote.Author = "";
						jsonNote.Text = "";
						jsonNote.Title = "";	
						$form[0].reset();														
					}
					else {

						$message.text(data.ErrorMessage);								
					}

					$message.fadeIn(500,function () { $message.fadeOut(2000); });																
				}
    	});

    });

});