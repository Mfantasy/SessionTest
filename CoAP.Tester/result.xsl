<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet  version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:msfunction="http://www.mycompany.org/ns/function" exclude-result-prefixes="msxsl msfunction">
  <xsl:output method='html' version='1.0' encoding='UTF-8' indent='yes'/>
  <xsl:template name="DOCTYPE">
    <![CDATA[<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">]]>
  </xsl:template>
  <msxsl:script implements-prefix="msfunction" language="javascript">
    <![CDATA[
function clock(){
    var time = new Date();
    var year = time.getFullYear();
    var month = time.getMonth() + 1;
    var day = time.getDate();
    var hours = time.getHours();
    var min = time.getMinutes();
    var sec = time.getSeconds();
    return year + "/" + month + "/" + day + " " + hours + ":" + min + ":" + sec ;}
]]>
  </msxsl:script>
  <xsl:template match="/">
    <xsl:value-of select="document('')/*/xsl:template[@name='DOCTYPE']/node()" disable-output-escaping="yes"/>
    <html>
      <head>
        <meta http-equiv="Content-type" content="text/html; charset=utf-8" />
        <title>CoAP协议测试结果</title>
      </head>
      <body>
        <div>
          <h2 style="text-align:center" >CoAP协议测试结果</h2>
          <div style="width:800px;margin:0 auto;text-align:left;" >
          <p style="margin:0 auto;width:200px;padding-left:400px">
            <xsl:value-of select="msfunction:clock()" />
          </p>
            </div>
        </div>
        <table border="1" style="border-collapse:collapse" width="800" align="center" cellpadding="4" cellspacing="0" bordercolor="#000000">
          <tr>
            <th>
              测试用例
            </th>
            <th colspan='2'>测试步骤/错误项名称</th>
            <th>期望</th>
            <th>接收</th>
            <th>错误信息</th>
          </tr>
          <xsl:for-each select="root/testcaseresult">
            <xsl:variable name="childCount" select="1" />
            <xsl:variable name="this" select="." />
            <xsl:variable name="stepCount" select="count(child::*/child::request)+count(child::*/child::response)" />
            <xsl:variable name="allitemCount" select="count(child::*/child::*/child::*//child::item)" />
            <xsl:variable name="uppercase">ABCDEFGHIJKLMNOPQRSTUVWXYZ</xsl:variable>
            <xsl:variable name="lowercase">abcdefghijklmnopqrstuvwxyz</xsl:variable>
            <!--<xsl:value-of select="count(child::*/child::request)+count(child::*/child::response)+count(child::*/child::*/child::*//child::item)"/>-->
            <tr >
              <xsl:if test="translate($this/result, $lowercase, $uppercase)='FALSE'" >
                <xsl:attribute name="bgcolor" >#D05353</xsl:attribute>
              </xsl:if>
              <xsl:if test="translate($this/result, $lowercase, $uppercase)='TRUE'" >
                <xsl:attribute name="bgcolor" >#ABCE00</xsl:attribute>
              </xsl:if>
              <td>
                <xsl:value-of select="testcasename"/>
              </td>
              <td colspan='2'></td>              
              <td></td>
              <td></td>
              <td></td>              
            </tr>
            
            <xsl:if test='number($stepCount) > 0'>
              <xsl:for-each select='child::testcaseitem/request|child::testcaseitem/response'>
                <xsl:variable name='step' select='.'></xsl:variable>
                <xsl:variable name='requestName' select='.'/>
                <xsl:variable name='responseName' select='./name'></xsl:variable>
                <xsl:variable name="itemCount" select="count(child::*//child::item)" />
                
              <tr>                
                <xsl:if test='position() = 1'>
                <td>                  
                  <xsl:attribute name="rowspan" >
                    <xsl:value-of select="$stepCount+$allitemCount"/>
                  </xsl:attribute>
                  步骤
                </td>
                </xsl:if>
                <td colspan='2'>
                  <xsl:if test="name() = 'request'">
                    <xsl:value-of select='$requestName'/>
                  </xsl:if>
                  <xsl:if test="name() = 'response'">
                    <xsl:value-of select='$responseName'/>
                  </xsl:if>
                </td>
                <td></td>
                <td></td>
                <td></td>                
              </tr>
                <xsl:if test="name() = 'response'">
                  <xsl:for-each select="responseitem/item">
                <tr>
                  <xsl:if test='position() = 1'>
                  <td>
                    <xsl:attribute name="rowspan" >
                      <xsl:value-of select="$itemCount"/>
                    </xsl:attribute>
                    错误项
                  </td>
                  </xsl:if>
                  <td>
                    <xsl:value-of select="names"/>
                  </td>
                  <td>
                    <xsl:value-of select="expected"/>
                  </td>
                  <td>
                    <xsl:value-of select="received"/>
                  </td>
                  <td>
                    <xsl:value-of select="errorinfo"/>
                  </td>
                </tr>
                  </xsl:for-each>
                </xsl:if>
              </xsl:for-each>
            </xsl:if>
          </xsl:for-each>
          <tr>
            <td colspan="6" >
              <xsl:variable name="uppercase">ABCDEFGHIJKLMNOPQRSTUVWXYZ</xsl:variable>
              <xsl:variable name="lowercase">abcdefghijklmnopqrstuvwxyz</xsl:variable>
              成功<xsl:value-of select="count(root/testcaseresult[translate(result, $lowercase, $uppercase)='TRUE'])"></xsl:value-of>
              个，失败
              <xsl:value-of select="count(root/testcaseresult[translate(result, $lowercase, $uppercase)='FALSE'])"></xsl:value-of>
              个
            </td>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>

