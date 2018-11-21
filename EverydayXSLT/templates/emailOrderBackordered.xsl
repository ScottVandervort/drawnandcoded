<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<!-- Import templates from other files. This must come FIRST in your root XSLT document. -->
	<xsl:include href="global.xsl" />
	<xsl:include href="datetime.xsl" />
	<xsl:include href="address.xsl" />
	<xsl:include href="enumeration.xsl" />
	<xsl:include href="order.xsl" />	
	<xsl:output method="html"/>	
		
	<xsl:template match="/Order">
		<html>
			<head>				
				<style type="text/css">
				
					body 
					{
						text-align: center;
						min-width: 750px;
					}		
						
						body span#orderNumber
						{
							float: right;
						}	
						
						body div.indent
						{
							margin-left: 20px;					
						}					
						
						body div#main 
						{
							text-align: left;
							width: 750px;
							margin-left: auto;
							margin-right: auto;
						}
												
							body div#main table#orderItems
							{
								border-color: black;
								border-width: 1px 0px 1px 0px;
								border-style: solid;
							}	
						
								body div#main table#orderItems th
								{		
									padding-left: 8px;
								}
							
								body div#main table#orderItems td 
								{
									padding-left: 8px;
								}						
				
				</style>
			</head>
			<body>
				<div id="main">
	
					<xsl:call-template name="renderUniveralDate">
						<xsl:with-param name="dateTime" select="@SaleDate" />
					</xsl:call-template>
	
					<span id="orderNumber">Order # <xsl:value-of select="@OrderNumber"/></span>
		
					<p>Dear <xsl:value-of select="Customer/@FirstName"/><xsl:text> </xsl:text><xsl:value-of select="Customer/@LastName"/>,</p>
		
					<p>The following items are on back order and have not been shipped.</p>
		
					<div class="indent">
						<xsl:call-template name="renderOrderItems">          
							<xsl:with-param name="orderItems" select="OrderItems"/>		 
							<xsl:with-param name="searchStatus" select="'ONORD'" />
						</xsl:call-template>			
					</div>
													
					<p>Sincerely,</p>
					<p>TestCo</p>
						
				</div>
			</body>		
		</html>
	</xsl:template>
	
</xsl:stylesheet>


