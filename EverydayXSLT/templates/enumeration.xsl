<!-- 	
	Enumeration (Domain Table) Templates 

	These templates are used to render a domain table / enumeration value.
	
	Requires :
		
			global.xsl
-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">	
	<xsl:output method="html"/>
	
	<!--
		Renders an Order Item's status.
		Params :	status - An <orderItem> status value.
	-->			
	<xsl:template name="renderOrderItemStatus">
		<xsl:param name="status" />
		<xsl:choose>
			<xsl:when test="translate($status,$smallcase,$uppercase) = 'SHIP'">						
				<xsl:text>Shipped</xsl:text>
			</xsl:when>
			<xsl:when test="translate($status,$smallcase,$uppercase) = 'ONORD'">						
				<xsl:text>On Order</xsl:text>
			</xsl:when>						          
			<xsl:otherwise>
				<xsl:text>Unknown</xsl:text>						
			</xsl:otherwise>
		</xsl:choose>					
	</xsl:template>	
	
</xsl:stylesheet>