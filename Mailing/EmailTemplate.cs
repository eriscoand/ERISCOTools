using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ERISCOTools.Mailing
{
    public class EmailTemplate
    {
        public HtmlString HTML_BEGIN { get; set; }
        public HtmlString LOGO { get; set; }
        public HtmlString HTML_END { get; set; }

        public EmailTemplate(string TITLE, string DOMAIN, string LOGO_URL)
        {
            this.LOGO = new HtmlString(@"<center><p class='col-sm-12'><img src='" + LOGO_URL + "' width='200px' /></p></center>");
            this.HTML_END = new HtmlString(@"</td></tr><tr><td><br><br><br><b><a href='" + DOMAIN + "'>" + TITLE + "</a></b></td></tr></tbody></table></div><!-- /content --></td><td></td></tr></tbody></table><!-- /body --></body></html>");
            this.HTML_BEGIN = new HtmlString(@"<!DOCTYPE html><html><head>
                                                <meta name='viewport' content='width=device-width'>
                                                <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
                                                <style>
                                                * {
                                                    font-family: Arial, sans-serif;
                                                    font-size: 100%;
                                                    line-height: 1.6em;
                                                    margin: 0;
                                                    padding: 0;
                                                }
                                                img {
                                                    max-width: 600px;
                                                    width: 100%;
                                                }
                                                body {
                                                    -webkit-font-smoothing: antialiased;
                                                    height: 100%;
                                                    -webkit-text-size-adjust: none;
                                                    width: 100% !important;
                                                    font-family: Arial, sans-serif;
                                                    font-size:12px;
                                                }
                                                a {
                                                    color: #348eda;
                                                }
                                                .last {
                                                    margin-bottom: 0;
                                                }
                                                .first {
                                                    margin-top: 0;
                                                }
                                                .padding {
                                                    padding: 10px 0;
                                                }
                                                table.body-wrap {
                                                    padding: 20px;
                                                    width: 100%;
                                                }
                                                table.body-wrap .container {
                                                    border: 1px solid #f0f0f0;
                                                }
                                                table.footer-wrap {
                                                    clear: both !important;
                                                    width: 100%;  
                                                }
                                                .footer-wrap .container p {
                                                    color: #666666;
                                                    font-size: 12px;
                                                }
                                                table.footer-wrap a {
                                                    color: #999999;
                                                }
                                                h1, 
                                                h2, 
                                                h3 {
                                                    color: #111111;
                                                    font-family: Arial, 'Lucida Grande', sans-serif;
                                                    font-weight: 200;
                                                    line-height: 1.2em;
                                                    margin: 40px 0 10px;
                                                }
                                                h1 {
                                                    font-size: 36px;
                                                }
                                                h2 {
                                                    font-size: 28px;
                                                }
                                                h3 {
                                                    font-size: 22px;
                                                }
                                                p, 
                                                ul, 
                                                ol {
                                                    font-size: 12px;
                                                    font-weight: normal;
                                                    margin-bottom: 10px;
                                                }
                                                ul li, 
                                                ol li {
                                                    margin-left: 5px;
                                                    list-style-position: inside;
                                                }
                                                /* Set a max-width, and make it display as block so it will automatically stretch to that width, but will also shrink down on a phone or something */
                                                .container {
                                                    clear: both !important;
                                                    display: block !important;
                                                    Margin: 0 auto !important;
                                                    max-width: 600px !important;
                                                }

                                                /* Set the padding on the td rather than the div for Outlook compatibility */
                                                .body-wrap .container {
                                                    padding: 20px;
                                                }

                                                /* This should also be a block element, so that it will fill 100% of the .container */
                                                .content {
                                                    display: block;
                                                    margin: 0 auto;
                                                    max-width: 600px;
                                                }

                                                /* Let's make sure tables in the content area are 100% wide */
                                                .content table {
                                                    width: 100%;
                                                }
                                                .button {
                                                    background-color: #f44336; /* RED */
                                                    border: none;
                                                    color: white;
                                                    padding: 30px;
                                                    margin: 30px;
                                                    text-align: center;
                                                    text-decoration: none;
                                                    display: inline-block;
                                                    font-size: 16px;
                                                }
                                                </style>
                                            </head>
                                            <body bgcolor='#f6f6f6'>
                                                <!-- body -->
                                                <table class='body-wrap' bgcolor='#f6f6f6'>
                                                  <tbody><tr>
                                                    <td></td>
                                                    <td class='container' bgcolor='#FFFFFF'>
                                                      <!-- content -->
                                                      <div class='content'>
                                                      <table>
                                                        <tbody><tr>
                                                          <td>");
        }
        
    }
}

