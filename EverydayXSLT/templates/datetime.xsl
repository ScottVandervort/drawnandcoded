<!-- 	
	Date/Time templates 

	These templates are used to render dates and times.
	
-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">	
	<xsl:output method="html"/>

	<!--
		Renders the specified universal date as a user-friendly date, "January, 1, 2011".
		Params :	dateTime - A universal date. Example : 2007-11-14T12:01:00
	-->		
	<xsl:template name="renderUniveralDate">
		<xsl:param name="dateTime" />
		<xsl:variable name="date" select="substring-before($dateTime, 'T')" />
		<xsl:variable name="year" select="substring-before($date, '-')" />
		<xsl:variable name="month" select="substring-before(substring-after($date, '-'), '-')" />
		<xsl:variable name="day" select="substring-after(substring-after($date, '-'), '-')" />			
		
		<xsl:call-template name="renderMonth">
			<xsl:with-param name="month" select="$month" />
        </xsl:call-template>		
		
		<xsl:text> </xsl:text><xsl:value-of select="$day" /><xsl:text>, </xsl:text><xsl:value-of select="$year" />
	</xsl:template>	
	
	<!--
		Renders the specified integer as a month, "January, February, etc..."
		Params :	month - A month (1-12).
	-->		
	<xsl:template name="renderMonth">
		<xsl:param name="month" />
		<xsl:choose>
			<xsl:when test="$month = '1'">						
				<xsl:text>January</xsl:text>
			</xsl:when>
			<xsl:when test="$month = '2'">						
				<xsl:text>February</xsl:text>
			</xsl:when>
			<xsl:when test="$month = '3'">						
				<xsl:text>March</xsl:text>
			</xsl:when>				
			<xsl:when test="$month = '4'">						
				<xsl:text>April</xsl:text>
			</xsl:when>								
			<xsl:when test="$month = '5'">						
				<xsl:text>May</xsl:text>
			</xsl:when>									
			<xsl:when test="$month = '6'">						
				<xsl:text>June</xsl:text>
			</xsl:when>				
			<xsl:when test="$month = '7'">						
				<xsl:text>July</xsl:text>
			</xsl:when>						
			<xsl:when test="$month = '8'">						
				<xsl:text>August</xsl:text>
			</xsl:when>									
			<xsl:when test="$month = '9'">						
				<xsl:text>September</xsl:text>
			</xsl:when>									
			<xsl:when test="$month = '10'">						
				<xsl:text>October</xsl:text>
			</xsl:when>							
			<xsl:when test="$month = '11'">						
				<xsl:text>November</xsl:text>
			</xsl:when>										
			<xsl:when test="$month = '12'">						
				<xsl:text>December</xsl:text>
			</xsl:when>							
		</xsl:choose>					
	</xsl:template>
	
</xsl:stylesheet>