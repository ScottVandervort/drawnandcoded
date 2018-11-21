<!-- 	
	Order Templates 

	These templates are used to render <order> elements.
	
	Requires :
	
		global.xsl
		address.xsl
-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html"/>
	
	<!--
		Renders the Order's Items in a nicely formatted <table>.
		Params :	orderItems 		- An <orderItems> node.
					searchStatus	- Only <orderItem>'s with these status(es) will be rendered. Case-insensitive.
									- Example : "'ONORD,SHIP'" or "'SHIP'".
	-->		
	<xsl:template name="renderOrderItems">
		<xsl:param name="orderItems" />
		<xsl:param name="searchStatus" />
							
		<table id="orderItems">
			<thead>
				<tr>
					<th>Sku</th>
					<th>Description</th>
					<th>Status</th>
					<th>Sale Price</th>
					<th>Sale Quantity</th>
					<th>Shipping</th>
					<th>Subtotal</th>
					<th>Tax</th>
					<th>Total</th>
				</tr>
			</thead>
			<tbody>
		
		<!-- Iterate through all <orderItem> nodes that have the specified status. -->
		<xsl:for-each select="$orderItems/OrderItem[contains(
														translate($searchStatus,$smallcase,$uppercase),
														translate(@Status,$smallcase,$uppercase))]">		
			<!-- Render the <orderItem>'s attributes. -->													
			<tr>																
				<td><a href="{@Uri}"><xsl:value-of select="@Sku"/></a></td>
				<td><xsl:value-of select="@Name"/></td>
				<td>
					<xsl:call-template name="renderOrderItemStatus">
						<xsl:with-param name="status" select="@Status" />
					</xsl:call-template>				
				</td>				
				<td><xsl:value-of select="format-number(@Price, '$###0.00')"/></td>				
				<td><xsl:value-of select="@Quantity"/></td>				
				<td><xsl:value-of select="format-number(@Shipping, '$###0.00')"/></td>				
				<td><xsl:value-of select="format-number(@Subtotal, '$###0.00')"/></td>				
				<td><xsl:value-of select="format-number(@SalesTax, '$###0.00')"/></td>				
				<td><xsl:value-of select="format-number(@GrandTotal, '$###0.00')"/></td>											
			</tr>			
		</xsl:for-each>		
		</tbody>
		</table>					
	</xsl:template>	
		
</xsl:stylesheet>