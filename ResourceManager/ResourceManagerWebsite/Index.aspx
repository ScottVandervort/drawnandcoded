<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ScottsJewels.Web.UI.Index" MasterPageFile="~/Master.Master" %>
<%@ Register Assembly="StyleManager" TagPrefix="scottJewels"  Namespace="ScottsJewels.Web.UI"%>
<%@ Register TagPrefix="scottJewels" TagName="UserControl" Src="~/UserControl.ascx" %>

<asp:Content ContentPlaceHolderID="headPlaceholder" runat="server">


	<scottJewels:StyleManagerProxy runat="server">											
		<Resources>
			<scottJewels:Style Media="screen" Path="~/styles/Master1.css" />
			<scottJewels:Style Media="screen" Path="~/styles/Master2.css" />
		</Resources>		
		<CompositeResource>
			<Resources>
				<scottJewels:Style Media="screen" Path="~/styles/Page1.css" />
				<scottJewels:Style Media="screen" Path="~/styles/Page2.css" />
			</Resources>		
		</CompositeResource>
	</scottJewels:StyleManagerProxy>
	
</asp:Content>

<asp:Content ContentPlaceHolderID="bodyPlaceholder" runat="server">
	
	<div id="Page">This is the Page's Markup</div>

	<scottJewels:UserControl runat="server" />

</asp:Content>






