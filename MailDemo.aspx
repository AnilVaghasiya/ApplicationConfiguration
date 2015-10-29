 <div class="col-lg-6 pull-right">
                        <form role="form" id="frmCareer" class="form-horizontal" runat="server">
                        <div>
                            <h2>
                                POST A RESUME
                            </h2>
                            <div class="form-group">
                                <label for="lblSalutation" class="control-label col-sm-3">
                                    Salutation</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlSalution" runat="server" CssClass="form-control pos-left"
                                        autofocus="true">
                                        <asp:ListItem Text="Mr.">Mr.</asp:ListItem>
                                        <asp:ListItem Text="Mrs.">Mrs.</asp:ListItem>
                                        <asp:ListItem Text="Miss.">Miss.</asp:ListItem>
                                        <asp:ListItem Text="Dr.">Dr.</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="lblFirstName" class="col-sm-3 control-label">
                                    FirstName</label>
                                <div>
                                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                                        Text="*" ForeColor="Red" ErrorMessage="Please Enter First Name" ToolTip="Please Enter First Name"
                                        ValidationGroup="vgSummary" SetFocusOnError="true" CssClass="pull-left">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtFirstName" class="form-control" placeholder="First Name" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="lblLastName" class="col-sm-3 control-label">
                                    LastName</label>
                                <div>
                                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                                        SetFocusOnError="true" Text="*" ForeColor="Red" ErrorMessage="Please Enter Last Name"
                                        ToolTip="Please Enter Last Name" ValidationGroup="vgSummary" CssClass="pull-left"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtLastName" class="form-control" placeholder="Last Name" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="lblEmailAddress" class="col-sm-3 control-label">
                                    Email Address</label>
                                <div>
                                    <asp:RequiredFieldValidator ID="rfvEmailAddress" runat="server" ControlToValidate="txtEmailAddress"
                                        SetFocusOnError="true" Text="*" ForeColor="Red" ErrorMessage="Please Enter Email Address"
                                        ToolTip="Please Enter Email Address" ValidationGroup="vgSummary" CssClass="pull-left"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtEmailAddress" class="form-control" placeholder="Email Address"
                                        runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="lblContactNumber" class="col-sm-3 control-label">
                                    Contact No.</label>
                                <div>
                                    <asp:RequiredFieldValidator ID="rfvContactNumber" runat="server" ControlToValidate="txtContactNumber"
                                        SetFocusOnError="true" Text="*" ForeColor="Red" ErrorMessage="Please Enter Contact Number"
                                        ToolTip="Please Enter Contact Number" ValidationGroup="vgSummary" CssClass="pull-left"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtContactNumber" class="form-control" placeholder="Contact Number"
                                        runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="lblCity" class="col-sm-3 control-label">
                                    City
                                </label>
                                <div>
                                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity"
                                        SetFocusOnError="true" Text="*" ForeColor="Red" ErrorMessage="Please Enter City"
                                        ToolTip="Please Enter City" ValidationGroup="vgSummary" CssClass="pull-left"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtCity" class="form-control" placeholder="City" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="lblQualification" class="col-sm-3 control-label">
                                    Qualification
                                </label>
                                <div>
                                    <asp:RequiredFieldValidator ID="rfvQualification" runat="server" ControlToValidate="txtQualification"
                                        SetFocusOnError="true" Text="*" ForeColor="Red" ErrorMessage="Please Enter Qualification"
                                        ToolTip="Please Enter Qualification" ValidationGroup="vgSummary" CssClass="pull-left"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtQualification" class="form-control" placeholder="Qualification"
                                        runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="lblApplyFor" class="col-sm-3 control-label">
                                    Apply For
                                </label>
                                <div>
                                    <asp:CompareValidator ID="rfvApplyFor" runat="server" ValueToCompare="- SELECT -"
                                        ControlToValidate="ddlApplyFor" Operator="NotEqual" Text="*" ToolTip="Please Select the Post Applying For."
                                        ErrorMessage="Please Select the Post Applying For." SetFocusOnError="true" ForeColor="Red"
                                        ValidationGroup="vgSummary" CssClass="pull-left"></asp:CompareValidator>
                                </div>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlApplyFor" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="- SELECT -">- SELECT -</asp:ListItem>
                                        <asp:ListItem Text="Web Developer">Web Developer</asp:ListItem>
                                        <asp:ListItem Text="Windows Developer">Windows Developer</asp:ListItem>
                                        <asp:ListItem Text="Designer">Designer</asp:ListItem>
                                        <asp:ListItem Text="Technical Support">Technical Support</asp:ListItem>
                                        <%--<asp:ListItem Text="Mobile Application Developer">Mobile Application Developer(DevExtreme)</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="lblResume" class="col-sm-3 control-label">
                                    Upload Resume</label>
                                <div>
                                    <asp:RequiredFieldValidator ID="rfvuploadResume" runat="server" ControlToValidate="uploadResume"
                                        SetFocusOnError="true" Text="*" ForeColor="Red" ErrorMessage="File Required !"
                                        Display="Dynamic" ToolTip="File Required !" ValidationGroup="vgSummary" CssClass="pull-left"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revuploadResume" runat="server" ValidationExpression="^.*\.(doc|DOC|docx|DOCX|pdf|PDF)$"
                                        ControlToValidate="uploadResume" ValidationGroup="vgSummary" ErrorMessage="Please Select PDF or Docx File"
                                        ToolTip="Please Select PDF or DOC File" Text="*" Display="Dynamic" ForeColor="Red"
                                        CssClass="pull-left"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-sm-6">
                                    <asp:FileUpload ID="uploadResume" runat="server" placeholder="Resume" CssClass="form-control pos-left" />
                                    <h4 style="color: Red; text-align: left; font-weight: normal;">
                                        (PDF or DOC)</h4>
                                    <%--<input type="file" class="form-control" id="field-file" placeholder="Resume">--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="lblRemarks" class="col-sm-3 control-label" style="font-weight: normal;">
                                    Remarks
                                </label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtRemarks" TextMode="MultiLine" class="form-control pos-left" placeholder="Remarks"
                                        Rows="5" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="lblReferenceName" class="col-sm-3 control-label" style="font-weight: normal;">
                                    Reference Name
                                </label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtReferencename" class="form-control pos-left" placeholder="Reference Name"
                                        runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="lblReferencecontact" class="col-sm-3 control-label" style="font-weight: normal;">
                                    Reference Contact
                                </label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtReferenceContact" class="form-control pos-left" placeholder="Reference Contact"
                                        runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-3 col-sm-3">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Save"
                                    AutoPostBack="false" OnClick="btnSubmit_Click" ValidationGroup="vgSummary" />
                            </div>
                        </div>
                        <div>
                            <asp:ValidationSummary ID="vsSummary" runat="server" ValidationGroup="vgSummary"
                                ShowMessageBox="true" ShowSummary="false" />
                        </div>
                        </form>
                    </div>
