<%@ Page Title="" Language="C#" MasterPageFile="~/Foodserver.master" AutoEventWireup="true" CodeFile="Payment.aspx.cs" Inherits="Payment" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cph_MainContent" runat="Server" ClientIDMode="Static">
    <div class="col-md-9 col-md-pull-3">
        <div class="box">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Krediet opladen</h3>
                </div>
                <div class="panel-body">
                    <p class="alert alert-danger">
                        Opgelet! <br />De kosten voor paypal (€0.35 + 3.4% van het orderbedrag) zullen in vermindering gebracht worden van het toegevoegde krediet.
                    </p>
                    </form>  
    <form action="https://www.sandbox.paypal.com/cgi-bin/webscr" method="post" target="_top">
        <input type="hidden" name="cmd" value="_s-xclick"/>
        <input type="hidden" name="hosted_button_id" value="5Z3G6XLHVEDUN"/>
        <table style="width:100%">
            <tr>
                <td>
                    <input type="hidden" name="on0" value="Bedrag krediet"/>
                    <h4>Bedrag krediet</h4>
                </td>
            </tr>
            <tr>
                <td>
                    <select name="os0">
                        <option value="5 euro">5 euro €5,54 EUR</option>
                        <option value="10 euro">10 euro €10,71 EUR</option>
                        <option value="20 euro">20 euro €21,07 EUR</option>
                        <option value="50 euro">50 euro €52,12 EUR</option>
                    </select>
                </td>
            </tr>
        </table>
        <input type="hidden" name="currency_code" value="EUR"/>
        <input type="hidden" name="custom" id="custom" runat="server" />
        <input type="image" src="https://www.sandbox.paypal.com/nl_NL/NL/i/btn/btn_buynowCC_LG.gif" border="0" name="submit" alt="PayPal, de veilige en complete manier van online betalen."/>
        <img alt="" border="0" src="https://www.sandbox.paypal.com/nl_NL/i/scr/pixel.gif" width="1" height="1"/>
    </form>
    <script src="http://ajax.microsoft.com/ajax/jquery/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            $("#custom").attr("name", "custom");
        });
    </script>
                    <form>
                </div>
            </div>
        </div>
    </div>

</asp:Content>






