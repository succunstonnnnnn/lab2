<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="/Scientists">
        <html>
            <head><title>Список науковців</title></head>
            <body>
                <h2>Список науковців</h2>
                <table border="1">
                    <tr>
                        <th>П.І.Б.</th>
                        <th>Факультет</th>
                        <th>Кафедра</th>
                        <th>Науковий ступінь</th>
                        <th>Вчене звання</th>
                        <th>Дата присвоєння звання</th>
                    </tr>
                    <xsl:for-each select="Scientist">
                        <tr>
                            <td><xsl:value-of select="@FullName"/></td>
                            <td><xsl:value-of select="@Faculty"/></td>
                            <td><xsl:value-of select="@Department"/></td>
                            <td><xsl:value-of select="@Degree"/></td>
                            <td><xsl:value-of select="@Rank"/></td>
                            <td><xsl:value-of select="@RankDate"/></td>
                        </tr>
                    </xsl:for-each>
                </table>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>