    protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //SendMail(txtEmailAddress.Text.Trim(), string strFromName, string strToEmail, string strToName, string strSubject, string strBody, string strAttachmentPath, string strAttachmentFile, string ReplyTo, string strBccMail);

            try
            {
                string EmailFrom = txtEmailAddress.Text.Trim();
                string Body = string.Empty;

                Body += "<br/> <br/>";
                Body += "<table cellspacing='0' cellpadding='2' width='50%'>";
                Body += " <tr><td colspan='2' align='center' style='border: 1px solid #DDDDDD;background-color: #F6EEEF'>Career</td></tr>";
                Body += "<tr> <td style='border: 1px solid #DDDDDD'><b>Name : </b></td><td style='border: 1px solid #DDDDDD'>" + ddlSalution.SelectedItem.Text.Trim() + " " + txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim() + "</td></tr>";
                Body += "<tr> <td style='border: 1px solid #DDDDDD'><b>EmailID :</b></td><td style='border: 1px solid #DDDDDD'>" + txtEmailAddress.Text.Trim() + "</td>";
                Body += "<tr> <td style='border: 1px solid #DDDDDD'><b>Contact Number : </b></td><td style='border: 1px solid #DDDDDD'>" + txtContactNumber.Text.Trim() + "</td></tr>";
                Body += "<tr> <td style='border: 1px solid #DDDDDD'><b>City :</b></td><td style='border: 1px solid #DDDDDD'>" + txtCity.Text.Trim() + "</td></tr>";

                Body += "<tr> <td style='border: 1px solid #DDDDDD'><b>Qualification :</b></td><td style='border: 1px solid #DDDDDD'>" + txtQualification.Text.Trim() + "</td></tr>";
                Body += "<tr> <td style='border: 1px solid #DDDDDD'><b>Job Post : </b></td><td style='border: 1px solid #DDDDDD'>" + ddlApplyFor.SelectedItem.Text.Trim() + "</td></tr>";
                Body += "</td></tr><tr><td style='border: 1px solid #DDDDDD'><b>Remarks : </b></td><td style='border: 1px solid #DDDDDD'>" + txtRemarks.Text.Trim() + "</td></tr>";
                Body += "</td></tr><tr><td style='border: 1px solid #DDDDDD'><b>Reference Name :</b></td><td style='border: 1px solid #DDDDDD'>" + txtReferencename.Text.Trim() + "</td></tr>";
                Body += "</td></tr><tr><td style='border: 1px solid #DDDDDD'><b>Reference Contact :</b></td><td style='border: 1px solid #DDDDDD'>" + txtReferenceContact.Text.Trim() + "</td></tr><table/>";

                Body += "<br><br/>Thanks<br/>";

                //string EmailTo = "hr@anandsystems.com";
                string EmailTo = ConfigurationManager.AppSettings["MailTo"];
                string Subject = ddlApplyFor.SelectedItem.Text.Trim();
                if (uploadResume.HasFile)
                {
                    try
                    {
                        string FilePath = System.Configuration.ConfigurationManager.AppSettings["FilePath"];
                        if (!Directory.Exists(FilePath))
                        {
                            Directory.CreateDirectory(FilePath);
                        }

                        string FileExtention = System.IO.Path.GetExtension(uploadResume.FileName);
                        uploadResume.SaveAs(FilePath + uploadResume.FileName);
                        //lblMessage.Text = "File name: " + uploadResume.PostedFile.FileName + "<br>" + uploadResume.PostedFile.ContentLength + " kb<br>" + "Content type: " + uploadResume.PostedFile.ContentType;
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "You have not specified a file.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Visible = true;
                        messagecurved.Style.Add("display", "block");
                    }
                }
                else
                {
                    lblMessage.Text = "You have not specified a file.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Visible = true;
                    messagecurved.Style.Add("display", "block");
                }
                if (!string.IsNullOrEmpty(EmailTo))
                {
                    if (!SendMail(EmailFrom, null, EmailTo, null, Subject, Body, ConfigurationManager.AppSettings["FilePath"], uploadResume.PostedFile.FileName, ConfigurationManager.AppSettings["ReplyTo"], ConfigurationManager.AppSettings["MailForBCC"]))
                    {
                        File.AppendAllText("D://Log.txt", "Failed To Receive your");
                        lblMessage.Text = "Failed To Receive your application.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Visible = true;
                        messagecurved.Style.Add("display", "block");
                    }
                    else
                    {
                        File.AppendAllText("D://Log.txt", "We have Received your application contact you soon");
                        lblMessage.Text = "We have Received your application contact you soon.";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Visible = true;
                        messagecurved.Style.Add("display", "block");
                    }
                }
                else
                {
                    File.AppendAllText("D://Log.txt", "Failed To Receive your application");
                    lblMessage.Text = "Failed To Receive your application";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Visible = true;
                    messagecurved.Style.Add("display", "block");
                }

                ClearControls();
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                File.AppendAllText("D://Log.txt", ex.ToString());
            }
        }

        public bool SendMail(string strFromEmail, string strFromName, string strToEmail, string strToName, string strSubject, string strBody, string strAttachmentPath, string strAttachmentFile, string ReplyTo, string strBccMail)
        {
            try
            {
                string body = strBody;

                if (!string.IsNullOrEmpty(strFromEmail) && !string.IsNullOrEmpty(strToEmail))
                {
                    MailMessage email = new MailMessage(strFromEmail, strToEmail);
                    // Information
                    if (!string.IsNullOrEmpty(strBccMail))
                    {
                        email.Bcc.Add(strBccMail);
                    }
                    File.AppendAllText("D://Log.txt", "strBccMail");
                    if (!string.IsNullOrEmpty(ReplyTo))
                    {
                        MailAddress MailReplyTo = new MailAddress(ReplyTo);
                        email.ReplyTo = MailReplyTo;
                    }
                    File.AppendAllText("D://Log.txt", "ReplyTo");
                    email.Subject = strSubject;
                    email.Body = body;
                    File.AppendAllText("D://Log.txt", strSubject);
                    AlternateView htmlView;
                    htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

                    // Add the views
                    email.AlternateViews.Add(htmlView);
                    File.AppendAllText("D://Log.txt", strAttachmentPath + strAttachmentFile);
                    //Add Attachment
                    if (!string.IsNullOrEmpty(strAttachmentPath) && !string.IsNullOrEmpty(strAttachmentFile) && File.Exists(strAttachmentPath + strAttachmentFile))
                    {
                        Attachment at = new Attachment(strAttachmentPath + strAttachmentFile);
                        at.Name = strAttachmentFile;
                        email.Attachments.Add(at);
                    }

                    string strSMTP = ConfigurationManager.AppSettings["CredentialSMTP"];
                    // Send the message
                    SmtpClient smtp = new SmtpClient(strSMTP); //specify the mail server address
                    System.Net.NetworkCredential nc = new System.Net.NetworkCredential();
                    nc.UserName = ConfigurationManager.AppSettings["SMTPUsername"];
                    nc.Password = ConfigurationManager.AppSettings["SMTPPassword"];
                    smtp.Credentials = nc;
                    smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["MailPort"]);
                    smtp.EnableSsl = true;

                    smtp.Send(email);
                    email.Dispose();
                    return true;
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText("D://Log.txt", ex.ToString());
                return false;
            }
            return false;
        }
