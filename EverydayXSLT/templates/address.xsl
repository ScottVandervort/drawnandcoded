<!-- 	
	Address Templates 

	These templates are used to render <address> elements.
	
	Requires :
	
		global.xsl
-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">	
	<xsl:output method="html"/>
												
	<!--
		Renders the specified Address with line breaks.
		Params :	address - An <address> node.
	-->	
	<xsl:template name="renderAddress">	
		<xsl:param name="address" />		
		<xsl:value-of select="$address/@FirstName"/><xsl:text> </xsl:text><xsl:value-of select="$address/@LastName"/>
		<br/>
		<xsl:value-of select="$address/@Line1"/>
		<xsl:if test="$address/@Line2 != ''">
			<br/>
			<xsl:value-of select="$address/@Line2"/>			
		</xsl:if>
		<br/>
		<xsl:value-of select="$address/@City"/><xsl:text>, </xsl:text><xsl:value-of select="$address/@State"/> 		
		<br/>
		<xsl:value-of select="$address/@Zip"/>		
	</xsl:template>	
				
</xsl:stylesheet>

