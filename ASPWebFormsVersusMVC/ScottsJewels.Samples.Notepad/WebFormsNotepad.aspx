<%@ Page Title="My Notepad (Web Forms)" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebFormsNotepad.aspx.cs" Inherits="ScottsJewels.Samples.Notepad.WebFormsNotepad" %>
<%@ Import Namespace="ScottsJewels" %>
<%@ Import Namespace="ScottsJewels.Samples.Notepad.DataModels" %>

<asp:Content ContentPlaceHolderID="headContent" runat="server">
    <script type="text/javascript" language ="javascript">			
		var jsonNote = <%= (new Note().ToJson() ) %>
	</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="bodyContent" runat="server">

	<form runat="server">				
		<div>
			<fieldset>									
				<asp:TextBox ID="searchAuthor" runat="server" />
				<asp:Button ID="getNotesByAuthor" OnClick="OnSearchAuthor" runat="server" Text="Search Author"/>				
			</fieldset>
		</div>
	</form>

	<form action="<%= ResolveUrl("~/Notepad/AddNote") %>" id="addNoteForm">
		<div>
			<fieldset>
				<legend>Add Note</legend>							
					<p>	
						<label for="author">Author :</label>
						<input style="position:absolute; left: 120px;" type="text" id="Author" name="Author"/>															
					</p>
					<p>				
						<label for="title">Title :</label>		
						<input style="position:absolute; left: 120px;" type="text" id="Title" name="Title"/>						
					</p>
					<p>				
						<label for="text">Text :</label>									
						<input style="position:absolute; left: 120px;" type="text" id="Text" name="Text"/>
					</p>				
					<p>
						<button type="submit" name="button" value="GetNotes">Add Note</button>
					</p>
			</fieldset>			
			<div id="message"></div>
		</div>
	</form>

	<div id="notes">	
<% 
		if (Model != null && Model.Count > 0)
		{			
			foreach (Note note in Model)
			{
%>			
		<div class="note">
			<span><%=note.Title%> by <%=note.Author%></span>
			<p>								
				<%=note.Text%>
			</p>						
		</div>
<%
			}
		}
%>
	</div>
</asp:Content>
