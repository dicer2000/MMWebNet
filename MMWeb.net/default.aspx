<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="MMWeb.net._default" %>

<%@ Register Src="~/controls/MMPos.ascx" TagPrefix="uc1" TagName="MMPos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://fonts.googleapis.com/css?family=Poppins:100,200,400,300,500,600,700" rel="stylesheet" />
    <!-- Bootstrap CDN -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" />
    <!-- Font Awesome CDN -->
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css" />
    <!-- Bootstrap-Iconpicker -->
    <link rel="stylesheet" href="css/bootstrap-iconpicker.min.css" />
    <!--
    <link rel="stylesheet" href="css/pricing.css" />
    -->

</head>
<body>
    <form id="form1" runat="server">

        <div class="d-flex flex-column flex-md-row align-items-center p-3 px-md-4 mb-3 bg-white border-bottom box-shadow">
            <h5 class="my-0 mr-md-auto font-weight-normal">Computer Science 320 - Database Management Systems</h5>
            <!--
      <nav class="my-2 my-md-0 mr-md-3">
        <a class="p-2 text-dark" href="#">Features</a>
        <a class="p-2 text-dark" href="#">Enterprise</a>
        <a class="p-2 text-dark" href="#">Support</a>
        <a class="p-2 text-dark" href="#">Pricing</a>
      </nav>
    -->
            <a class="btn btn-outline-primary" target="_blank" href="http://www.principiacollege.edu/computer-science">Find Out More</a>
        </div>

        <div class="pricing-header px-3 py-3 pb-md-4 mx-auto text-center">
            <h1 class="display-4">Master Mind</h1>



            <p class="lead">How fast can you solve Master Mind?  See if you can top today's leader board.</p>

        </div>

        <div class="container">
            <div class="card-deck mb-3 text-center">
                <div class="card mb-4 box-shadow">
                    <div class="card-header">
                        <h4 class="my-0 font-weight-normal">Today's Leader Board</h4>
                    </div>
                    <div class="card-body">
                        <!-- Leader Board -->

                        <asp:GridView ID="gvLeaders" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField ShowHeader="False" ItemStyle-CssClass="card-title pricing-card-title"></asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="card-title pricing-card-title"></asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                <asp:Panel ID="pnlName" Visible="true" runat="server" CssClass="card mb-4 mr-5 ml-5 box-shadow">
                    <div class="card-header">
                        <h4 class="my-0 font-weight-normal">It's Free!</h4>
                    </div>
                    <div class="card-body">
                        <h5>Player Name</h5>
                        <asp:TextBox ID="txtHandle" runat="server"></asp:TextBox>
                        <asp:Button ID="cmdGo" CssClass="btn btn-lg btn-block btn-primary mt-3" runat="server" Text="Go!" OnClick="cmdGo_Click"></asp:Button>
                    </div>
                </asp:Panel>


                <asp:HiddenField ID="hdnGameID" runat="server" />
                <asp:Panel ID="pnlGame" Visible="false" runat="server" CssClass="card mb-4 mr-5 ml-5 box-shadow">
                    <div class="card-header">
                        <h4 class="my-0 font-weight-normal">
                            <asp:Label ID="lblName" runat="server" Text="" CssClass="mr-5"></asp:Label>  1000 pts</h4>
                    </div>

                    <asp:GridView ID="gvGame" runat="server" AutoGenerateColumns="False" ShowHeader="False" OnRowCommand="gvGame_RowCommand">
                        <Columns>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("RowNo") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("RowNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <div class="btn-group">
                                        <asp:HiddenField runat="server" ID="hiddenPos0" ClientIDMode="Static" />
                                        <a id="dd0" class="btn dropdown-toggle btn-secondary" data-toggle="dropdown" href="#">
                                            <i id="di0" class="far fa-circle butt"></i>
                                        </a>
                                        <div class="dropdown-menu">
                                            <button id="p0c0" data-color="red" data-val="0" class="dropdown-item btn0" type="button"><i class="fas fa-circle butt" style="color: red"></i></button>
                                            <button id="p0c1" data-color="dodgerblue" data-val="1" class="dropdown-item btn0" type="button"><i class="fas fa-circle butt" style="color: dodgerblue"></i></button>
                                            <button id="p0c2" data-color="forestgreen" data-val="2" class="dropdown-item btn0" type="button"><i class="fas fa-circle butt" style="color: forestgreen"></i></button>
                                            <button id="p0c3" data-color="gold" data-val="3" class="dropdown-item btn0" type="button"><i class="fas fa-circle butt" style="color: gold"></i></button>
                                            <button id="p0c4" data-color="magenta" data-val="4" class="dropdown-item btn0" type="button"><i class="fas fa-circle butt" style="color: magenta"></i></button>
                                            <button id="p0c5" data-color="cyan" data-val="5" class="dropdown-item btn0" type="button"><i class="fas fa-circle butt" style="color: cyan"></i></button>
                                        </div>
                                    </div>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <uc1:MMPos runat="server" ID="MMPos1" CurrentState='<%# Eval("GuessPosition1") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <div class="btn-group">
                                        <asp:HiddenField runat="server" ID="hiddenPos1" ClientIDMode="Static" />
                                        <a id="dd1" class="btn dropdown-toggle btn-secondary" data-toggle="dropdown" href="#">
                                            <i id="di1" class="far fa-circle butt"></i>
                                        </a>
                                        <div class="dropdown-menu">
                                            <button id="p1c0" data-color="red" data-val="0" class="dropdown-item btn1" type="button"><i class="fas fa-circle butt" style="color: red"></i></button>
                                            <button id="p1c1" data-color="dodgerblue" data-val="1" class="dropdown-item btn1" type="button"><i class="fas fa-circle butt" style="color: dodgerblue"></i></button>
                                            <button id="p1c2" data-color="forestgreen" data-val="2" class="dropdown-item btn1" type="button"><i class="fas fa-circle butt" style="color: forestgreen"></i></button>
                                            <button id="p1c3" data-color="gold" data-val="3" class="dropdown-item btn1" type="button"><i class="fas fa-circle butt" style="color: gold"></i></button>
                                            <button id="p1c4" data-color="magenta" data-val="4" class="dropdown-item btn1" type="button"><i class="fas fa-circle butt" style="color: magenta"></i></button>
                                            <button id="p1c5" data-color="cyan" data-val="5" class="dropdown-item btn1" type="button"><i class="fas fa-circle butt" style="color: cyan"></i></button>
                                        </div>
                                    </div>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <uc1:MMPos runat="server" ID="MMPos2" CurrentState='<%# Eval("GuessPosition2") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <div class="btn-group">
                                        <asp:HiddenField runat="server" ID="hiddenPos2" ClientIDMode="Static" />
                                        <a id="dd2" class="btn dropdown-toggle btn-secondary" data-toggle="dropdown" href="#">
                                            <i id="di2" class="far fa-circle butt"></i>
                                        </a>
                                        <div class="dropdown-menu">
                                            <button id="p2c0" data-color="red" data-val="0" class="dropdown-item btn2" type="button"><i class="fas fa-circle butt" style="color: red"></i></button>
                                            <button id="p2c1" data-color="dodgerblue" data-val="1" class="dropdown-item btn2" type="button"><i class="fas fa-circle butt" style="color: dodgerblue"></i></button>
                                            <button id="p2c2" data-color="forestgreen" data-val="2" class="dropdown-item btn2" type="button"><i class="fas fa-circle butt" style="color: forestgreen"></i></button>
                                            <button id="p2c3" data-color="gold" data-val="3" class="dropdown-item btn2" type="button"><i class="fas fa-circle butt" style="color: gold"></i></button>
                                            <button id="p2c4" data-color="magenta" data-val="4" class="dropdown-item btn2" type="button"><i class="fas fa-circle butt" style="color: magenta"></i></button>
                                            <button id="p2c5" data-color="cyan" data-val="5" class="dropdown-item btn2" type="button"><i class="fas fa-circle butt" style="color: cyan"></i></button>
                                        </div>
                                    </div>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <uc1:MMPos runat="server" ID="MMPos3" CurrentState='<%# Eval("GuessPosition3") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <div class="btn-group">
                                        <asp:HiddenField runat="server" ID="hiddenPos3" ClientIDMode="Static" />
                                        <a id="dd3" class="btn dropdown-toggle btn-secondary" data-toggle="dropdown" href="#">
                                            <i id="di3" class="far fa-circle butt"></i>
                                        </a>
                                        <div class="dropdown-menu">
                                            <button id="p3c0" data-color="red" data-val="0" class="dropdown-item btn3" type="button"><i class="fas fa-circle butt" style="color: red"></i></button>
                                            <button id="p3c1" data-color="dodgerblue" data-val="1" class="dropdown-item btn3" type="button"><i class="fas fa-circle butt" style="color: dodgerblue"></i></button>
                                            <button id="p3c2" data-color="forestgreen" data-val="2" class="dropdown-item btn3" type="button"><i class="fas fa-circle butt" style="color: forestgreen"></i></button>
                                            <button id="p3c3" data-color="gold" data-val="3" class="dropdown-item btn3" type="button"><i class="fas fa-circle butt" style="color: gold"></i></button>
                                            <button id="p3c4" data-color="magenta" data-val="4" class="dropdown-item btn3" type="button"><i class="fas fa-circle butt" style="color: magenta"></i></button>
                                            <button id="p3c5" data-color="cyan" data-val="5" class="dropdown-item btn3" type="button"><i class="fas fa-circle butt" style="color: cyan"></i></button>
                                        </div>
                                    </div>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <uc1:MMPos runat="server" ID="MMPos4" CurrentState='<%# Eval("GuessPosition4") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:Button ID="cmdAddMove" CssClass="btn btn-lg btn-block btn-primary" runat="server" Text='Go' CommandName="AddMove" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:Button>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBlackPegs" runat="server" Text='<%# Eval("NumberCorrectPosition") %>'></asp:Label>
                                    /
                                                <asp:Label ID="lblWhitePegs" runat="server" Text='<%# Eval("NumberCorrectColor") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </asp:Panel>
            </div>
        </div>

        <footer class="pt-4 my-md-5 pt-md-5 border-top">
            <div class="row">
                <div class="col-1 col-md">
                    <small class="d-block mb-3 text-muted">&copy;2018-2019 Principia College.</small>
                </div>
            </div>
        </footer>

        <!-- Modal -->
        <div class="modal fade" id="modelWin" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLongTitle">Congratulations - You've Won!</h5>
                    </div>
                    <div class="modal-body">
                        You got it in xxx guesses.  Great job - that adds xxx to your daily score.
                        Hit 'New Game' to have another go.
                    </div>
                    <div class="modal-footer">
                        <asp:button id="cmdDone" runat="server" class="btn btn-secondary" data-dismiss="modal" OnClick="cmdDone_Click" Text="I'm Done"></asp:button>
                        <asp:button id="cmdNew" runat="server" class="btn btn-primary" OnClick="cmdNew_Click" text="New Game"></asp:button>
                    </div>
                </div>
            </div>
        </div>

        <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
        <script type="text/javascript" src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.bundle.min.js"></script>
        <script type="text/javascript">

            $(".btn0").click(function () {
                var color = jQuery(this).attr("data-color");
                var val = jQuery(this).attr("data-val");
                $('#hiddenPos0').val(val);
                $('#di0').attr('style', 'color: ' + color);
                $('#di0').attr('class', 'fas fa-circle butt');
            });

            $(".btn1").click(function () {
                var color = jQuery(this).attr("data-color");
                var val = jQuery(this).attr("data-val");
                $('#hiddenPos1').val(val);
                $('#di1').attr('style', 'color: ' + color);
                $('#di1').attr('class', 'fas fa-circle butt');
            });

            $(".btn2").click(function () {
                var color = jQuery(this).attr("data-color");
                var val = jQuery(this).attr("data-val");
                $('#hiddenPos2').val(val);
                $('#di2').attr('style', 'color: ' + color);
                $('#di2').attr('class', 'fas fa-circle butt');
            });

            $(".btn3").click(function () {
                var color = jQuery(this).attr("data-color");
                var val = jQuery(this).attr("data-val");
                $('#hiddenPos3').val(val);
                $('#di3').attr('style', 'color: ' + color);
                $('#di3').attr('class', 'fas fa-circle butt');
            });

        </script>

        <asp:Literal runat="server" ID="litWinDialog" Visible="false">
            <script type="text/javascript">

                $('#modelWin').modal({
                    keyboard: false
                });
            </script>
        </asp:Literal>



    </form>

</body>
</html>
