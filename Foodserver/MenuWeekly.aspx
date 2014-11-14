<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuWeekly.aspx.cs" Inherits="MenuWeekly" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Weekmenu</title>
    <link href="CSS/Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div id="row" class="row" runat="server">
                <div class="col-md-4 col-md-offset-2 divError">
                    <div id="panelRSS" class="panel panel-default" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">Weekmenu Rijselstraat</h3>
                        </div>
                        <div class="panel-body">
                            <div id="weekmenuRSS" runat="server">
                                <ol>
                                    <li>
                                        <div>
                                            <span>Ma</span>
                                            <span id="spanDagRSS1" class="spanDag" runat="server"></span>
                                        </div>
                                        <ul id="ulDagRSS1" runat="server"> 
                                        </ul>
                                    </li>
                                    <li>
                                        <div>
                                            <span>Di</span>
                                            <span id="spanDagRSS2" class="spanDag" runat="server"></span>
                                        </div>
                                        <ul id="ulDagRSS2" runat="server"></ul>
                                    </li>
                                    <li>
                                        <div>
                                            <span>Wo</span>
                                            <span id="spanDagRSS3" class="spanDag" runat="server"></span>
                                        </div>
                                        <ul id="ulDagRSS3" runat="server"></ul>
                                    </li>
                                    <li>
                                        <div>
                                            <span>Do</span>
                                            <span id="spanDagRSS4" class="spanDag" runat="server"></span>
                                        </div>
                                        <ul id="ulDagRSS4" runat="server"></ul>
                                    </li>
                                    <li>
                                        <div>
                                            <span>Vr</span>
                                            <span id="spanDagRSS5" class="spanDag" runat="server"></span>
                                        </div>
                                        <ul id="ulDagRSS5" runat="server"></ul>
                                    </li>
                                    <li style="font-size:12px; color:#555; border-bottom:none; height:15px;">
                                        Elke dag verse soep en desserts te koop aan de zelfbediening voor slechts €0.50!
                                    </li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 divError">
                    <div id="Div1" class="panel panel-default" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">Weekmenu Sint-Jorisstraat</h3>
                        </div>
                        <div class="panel-body">
                            <div id="weekmenuSJS" runat="server">
                                <ol>
                                    <li>
                                        <div>
                                            <span>Ma</span>
                                            <span id="spanDagSJS1" class="spanDag" runat="server"></span>
                                        </div>
                                        <ul id="ulDagSJS1" runat="server"> 
                                        </ul>
                                    </li>
                                    <li>
                                        <div>
                                            <span>Di</span>
                                            <span id="spanDagSJS2" class="spanDag" runat="server"></span>
                                        </div>
                                        <ul id="ulDagSJS2" runat="server"></ul>
                                    </li>
                                    <li>
                                        <div>
                                            <span>Wo</span>
                                            <span id="spanDagSJS3" class="spanDag" runat="server"></span>
                                        </div>
                                        <ul id="ulDagSJS3" runat="server"></ul>
                                    </li>
                                    <li>
                                        <div>
                                            <span>Do</span>
                                            <span id="spanDagSJS4" class="spanDag" runat="server"></span>
                                        </div>
                                        <ul id="ulDagSJS4" runat="server"></ul>
                                    </li>
                                    <li>
                                        <div>
                                            <span>Vr</span>
                                            <span id="spanDagSJS5" class="spanDag" runat="server"></span>
                                        </div>
                                        <ul id="ulDagSJS5" runat="server"></ul>
                                    </li>
                                    <li style="font-size:12px; color:#555; border-bottom:none; height:15px;">
                                        Elke dag verse soep en desserts te koop aan de zelfbediening voor slechts €0.50!
                                    </li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
