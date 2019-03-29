<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/">
    <html>
      <head>
        <style type="text/css">
          body{background-color: #000;}
          hr{border: 1px solid #FFF; height: 0;}
          table {border: 0 none; border-collapse: collapse; width: 100%;}
          table tr th, table tr td{ font-family: Consolas, SimSun, sans-serif; font-size: 16px; line-height: 1.25em; padding: 3px 0; text-align: left; vertical-align: top; }
          table tr th {color: #FF0; width: 120px;}
          table tr td {color: #0FF; }
          span.m_line{display: block; }
          span.st_line{color: #F00; display: block; font-size: 14px; padding-left: 3em; text-indent:-1.75em; }
        </style>
      </head>
      <body>
        <xsl:for-each select="Log/Exception">
          <table>
            <tr>
              <th>Date&amp;Time</th>
              <td>
                <xsl:value-of select="@Time"/>
              </td>
            </tr>
            <tr>
              <th>Exception</th>
              <td>
                <xsl:value-of select="@Type"/>
              </td>
            </tr>
            <tr>
              <th>IPAddress</th>
              <td>
                <xsl:value-of select="ClientIP"/>
              </td>
            </tr>
            <tr>
              <th>UserAgent</th>
              <td>
                <xsl:value-of select="ClientUA"/>
              </td>
            </tr>
            <tr>
              <th>URL</th>
              <td>
                <xsl:value-of select="ExactURL"/>
              </td>
            </tr>
            <tr>
              <th>Message</th>
              <td>
                <xsl:for-each select="Message/Line">
                  <span class="m_line">
                    <xsl:value-of select="."/>
                  </span>
                </xsl:for-each>
              </td>
            </tr>
            <tr>
              <th>Source</th>
              <td>
                <xsl:value-of select="Source"/>
              </td>
            </tr>
            <tr>
              <th colspan="2">StackTrace</th>
            </tr>
            <tr>
              <td colspan="2">
                <xsl:for-each select="StackTrace/Line">
                  <span class="st_line">
                    <xsl:value-of select="."/>
                  </span>
                </xsl:for-each>
              </td>
            </tr>
          </table>
          <hr />
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>
  
</xsl:stylesheet>