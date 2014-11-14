<%@ Page Language="C#" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>About Foodserver</title>
    <link href="CSS/Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div id="divAbout" class="col-md-6 col-md-offset-3">
                    <div class="panel panel-default">
                        <div class="panel-heading" id="HeadLogin">
                            <div class="panel-title">
                                Foodserver Legacy
                                <asp:LinkButton ID="lbtn_Back" runat="server" CssClass="backButton" OnClick="lbtn_Back_Click">Back</asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <h4>Auteurs en Release notes:</h4>
                            <ul>
                                <li>
                                    <b>Versie 7.0</b> (Mei 2014): Caenepeel Aaron, Schelstraete Bryan, Martelé Lorenzo, Gunst Niels
                                    <ul>
                                        <li>Mobile layout.</li>
                                        <li>Bug fixes</li>
                                        <li>Weekmenu</li>
                                        <li>Optimalisatie bestelprocedure</li>
                                    </ul>
                                </li>
                                <li>
                                    <b>Versie 6.0</b> (Maart 2014): Caenepeel Aaron, Schelstraete Bryan, Martelé Lorenzo, Gunst Niels
                                    <ul>
                                        <li>Layout aangepast.</li>
                                        <li>Bug fixes</li>
                                        <li>Unit tests</li>
                                        <li>Feedback overzicht (admins)</li>
                                        <li>Error overzicht (admins)</li>
                                        <li>E-mail bevestiging</li>
                                        <li>Besteluur: 10u30</li>
                                    </ul>
                                </li>
                                <li>
                                    <b>Versie 5.0</b> (Maart 2014): Caenepeel Aaron, Schelstraete Bryan, Martelé Lorenzo, Gunst Niels
                                    <ul>
                                        <li>Layout aangepast.</li>
                                        <li>Error's worden opgeslagen.</li>
                                        <li>Aangepaste error-pagina.</li>
                                        <li>Nieuw adres: <a href="http://resto.howest.be">resto.howest.be</a></li>
                                    </ul>
                                </li>
                                <li>
                                    <b>Versie 4.0</b> (December 2013): Vandenberghe Sam, Vergote Stijn, Vial Evgeni
                                    <ul>
                                        <li>Migratie naar campus St. Jorisstraat.</li>
                                        <li>E-mail bevestiging beta.</li>
                                        <li>Upgraden maaltijd administratie.</li>
                                        <li>Mogelijkheid om maaltijden te annuleren.</li>
                                    </ul>
                                </li>
                                <li>
                                    <b>Versie 3.0</b> (November 2013): Vandenberghe Sam, Vergote Stijn, Vial Evgeni
                                    <ul>
                                        <li>Visuele upgrade.</li>
                                        <li>Upgraden bestelsysteem (winkelmandje).</li>
                                        <li>Mogelijkheid tot uploaden weekmenu</li>
                                        <li>Back-end fixes.</li>
                                    </ul>
                                </li>
                                <li>
                                    <b>Versie 2.0</b> (Mei 2013): Claerbout Arne, Vandenberghe Sam, Vergote Stijn, Vial Evgeni
                                    <ul>
                                        <li>Mogelijkheid om broodjes samen te stellen.</li>
                                        <li>Upgrade van maaltijd administratie.</li>
                                        <li>Open versie voor publiek op campus RSS.</li>
                                    </ul>
                                </li>
                                <li>
                                    <b>Versie 1.0</b> (April 2013): Claerbout Arne, Vandenberghe Sam, Vergote Stijn, Vial Evgeni
                                    <ul>
                                        <li>Authenticatie systeem.</li>
                                        <li>Basis versie bestel systeem en administratie.</li>
                                        <li>Bèta versie opengesteld voor personeel en lectoren.</li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
