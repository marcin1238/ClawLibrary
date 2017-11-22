﻿-- USERS -------------------------------------------------------------------------------------------------------------------
INSERT INTO [dbo].[User] ([Key],[Language], [Email], [PasswordSalt],[PasswordHash],[FirstName],[LastName],[CreatedDate],[Status], [CreatedBy])VALUES(NEWID(),'PL','admin@test.com','1b568a7c-61cf-415c-b293-dcf40362192c','AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==','Admin','Admin',SYSDATETIMEOFFSET(),'Active','System');

-- ROLES -------------------------------------------------------------------------------------------------------------------
INSERT INTO [dbo].[Role]([Key],[Name],[CreatedDate],[CreatedBy],[ModifiedDate],[ModifiedBy],[Status])VALUES(NEWID(),'Admin',SYSDATETIMEOFFSET(),'System',null,null,'Active');
INSERT INTO [dbo].[Role]([Key],[Name],[CreatedDate],[CreatedBy],[ModifiedDate],[ModifiedBy],[Status])VALUES(NEWID(),'Regular',SYSDATETIMEOFFSET(),'System',null,null,'Active');

-- USER ROLE -------------------------------------------------------------------------------------------------------------------
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[CreatedBy],[ModifiedDate],[ModifiedBy],[Status])
     VALUES(NEWID(),1,1,SYSDATETIMEOFFSET(),'System',null,null,'Active');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[CreatedBy],[ModifiedDate],[ModifiedBy],[Status])
     VALUES(NEWID(),1,2,SYSDATETIMEOFFSET(),'System',null,null,'Active');

-- EMAIL TEMPLATE -------------------------------------------------------------------------------------------------------------------
	 INSERT INTO [EmailTemplate]([Name], [Content]) VALUES('AccountVerificationTemplate', 
'<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>A Simple Responsive HTML Email</title>
        <style type="text/css">
        body {
			margin: 0; padding: 0; min-width: 100%!important;
		}
        .content {
			width: 100%; max-width: 600px;
		}  
		@media only screen and (min-device-width: 601px) {
			.content {width: 600px !important;}
			.col425 {width: 425px!important;}
			.col380 {width: 380px!important;}
		}
		.header {
			padding: 0px 0px 0px 0px;
		}
		.subhead {
			font-size: 15px; 
			color: #ffffff; 
			font-family: sans-serif; 
			letter-spacing: 10px;
		}
		.h1 {
			font-size: 33px; 
			line-height: 38px; 
			font-weight: bold;
		}
		.h1, .h2, .bodycopy {
			color: #153643; 
			font-family: sans-serif;
		}			
		.innerpadding {
			padding: 30px 30px 30px 30px;
			background-color: #ffffff;
		}
		.borderbottom {
			border-bottom: 1px solid #ffffff;
		}
		.h2 {
			padding: 0 0 15px 0; 
			font-size: 24px; 
			line-height: 28px; 
			font-weight: bold;
		}
		.bodycopy {
			font-size: 16px; 
			line-height: 22px;
		}

		.button {
			text-align: center; 
			font-size: 18px; 
			font-family: sans-serif; 
			font-weight: bold; 
			padding: 0 30px 0 30px;
		}
		.button a {
			color: #ffffff; 
			text-decoration: none;
		}

		.footer {
			padding: 20px 30px 15px 30px;
		}
		.footercopy {
			font-family: sans-serif; 
			font-size: 14px; 
			color: #888;
		}
		.footercopy a {
			color: #888;
			text-decoration: underline;
		}
        </style>
		
    </head>
    <body yahoo bgcolor="#efefef">
        <table width="100%" bgcolor="#efefef" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
				<!--[if (gte mso 9)|(IE)]>
					<table width="600" align="center" cellpadding="0" cellspacing="0" border="0">
						<tr>
							<td>
								<![endif]-->
									<table class="content" align="center" cellpadding="0" cellspacing="0" border="0">
										<tr>
											<td class="header" bgcolor="#ffffff">
												<table height="190" align="left" border="0" cellpadding="0" cellspacing="0">
													<tr>
														<td height="190" style="padding: 0 0 0 0;">
															<img src="cid:{banner}" width="100%" height="100%" border="0" alt="" />
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td class="innerpadding borderbottom">
												<table width="100%" border="0" cellspacing="0" cellpadding="0">
													<tr>
														<td class="h2">
															Witaj {receiverName},
														</td>
													</tr>
												    <tr>
												        <td class="bodycopy">
												            Dziękujemy za Twoją rejestrację na platformie ClawLibrary.
												        </td>
												    </tr>
												    <tr>
												        <td class="bodycopy">
                                                            Aby móc korzystać z naszego portalu potwierdź swój adres e-mail klikając w przycisk poniżej.
												        </td>
												    </tr>
												</table>
											</td>
										</tr>
									    <tr>
									        <td class="innerpadding borderbottom">
									            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">  
									                <tr>
									                    <td align="center">
									                        <table class="buttonwrapper" bgcolor="#e05443" border="0" cellspacing="0" cellpadding="0">
									                            <tr>
									                                <td class="button" height="45">
									                                    <a href="{baseUrl}/#!/verify/{userKey}">Zweryfikuj konto</a>
									                                </td>
									                            </tr>
									                        </table>
									                    </td>
									                </tr>
									            </table>
									        </td>
									    </tr>
									    <tr>
									        <td class="innerpadding borderbottom">
									            <table width="100%" border="0" cellspacing="0" cellpadding="0">
									                <tr>
									                    <td class="bodycopy">
									                        Obsługa klienta,
									                    </td>
									                </tr>
									                <tr>
									                    <td class="bodycopy">
									                        W przypadku pytań, propozycji lub problemów prosimy o kontakt z naszym zespołem na adres kontakt@clawlibrary.com.
									                    </td>
									                </tr>
									                <tr height="25"></tr>
									                <tr>
									                    <td class="bodycopy">
									                        Serdecznie pozdrawaimy,
									                    </td>
									                </tr>
									                <tr>
									                    <td class="bodycopy">
									                        Twój zespół ClawLibrary
									                    </td>
									                </tr>
									            </table>
									        </td>
									    </tr>
										<tr>
											<td class="footer" >
												<table width="100%" border="0" cellspacing="0" cellpadding="0">
													<tr>
														<td align="center" class="footercopy">
															Wysłane przez ClawLibrary, ul. Sienkiewicza 82/84 | 90-318 Łódź<br/>
														    kontakt@clawlibrary.com
														</td>
													</tr>
													<tr>
														<td align="center" style="padding: 20px 0 0 0;">
															<table border="0" cellspacing="0" cellpadding="0">
																<tr>
																	<td width="37" style="text-align: center; padding: 0 10px 0 10px;">
																		<a href="http://www.facebook.com/">
																			<img src="cid:{facebook}" width="37" height="37" alt="Facebook" border="0" />
																		</a>
																	</td>
																	<td width="37" style="text-align: center; padding: 0 10px 0 10px;">
																		<a href="http://www.twitter.com/">
																			<img src="cid:{twitter}" width="37" height="37" alt="Twitter" border="0" />
																		</a>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</td>
										</tr>	
									</table>
								<!--[if (gte mso 9)|(IE)]>
							</td>
						</tr>
					</table>
				<![endif]-->
                </td>
            </tr>
        </table>
    </body>
</html>')

INSERT INTO [EmailTemplate]([Name], [Content]) VALUES('ResetPasswordTemplate', 
'<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>A Simple Responsive HTML Email</title>
        <style type="text/css">
        body {
			margin: 0; padding: 0; min-width: 100%!important;
		}
        .content {
			width: 100%; max-width: 600px;
		}  
		@media only screen and (min-device-width: 601px) {
			.content {width: 600px !important;}
			.col425 {width: 425px!important;}
			.col380 {width: 380px!important;}
		}
		.header {
			padding: 0px 0px 0px 0px;
		}
		.subhead {
			font-size: 15px; 
			color: #ffffff; 
			font-family: sans-serif; 
			letter-spacing: 10px;
		}
		.h1 {
			font-size: 33px; 
			line-height: 38px; 
			font-weight: bold;
		}
		.h1, .h2, .bodycopy {
			color: #153643; 
			font-family: sans-serif;
		}			
		.innerpadding {
			padding: 30px 30px 30px 30px;
			background-color: #ffffff;
		}
		.borderbottom {
			border-bottom: 1px solid #ffffff;
		}
		.h2 {
			padding: 0 0 15px 0; 
			font-size: 24px; 
			line-height: 28px; 
			font-weight: bold;
		}
		.bodycopy {
			font-size: 16px; 
			line-height: 22px;
		}

		.button {
			text-align: center; 
			font-size: 18px; 
			font-family: sans-serif; 
			font-weight: bold; 
			padding: 0 30px 0 30px;
		}
		.button a {
			color: #ffffff; 
			text-decoration: none;
		}

		.footer {
			padding: 20px 30px 15px 30px;
		}
		.footercopy {
			font-family: sans-serif; 
			font-size: 14px; 
			color: #888;
		}
		.footercopy a {
			color: #888;
			text-decoration: underline;
		}
        </style>
		
    </head>
    <body yahoo bgcolor="#efefef">
        <table width="100%" bgcolor="#efefef" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
				<!--[if (gte mso 9)|(IE)]>
					<table width="600" align="center" cellpadding="0" cellspacing="0" border="0">
						<tr>
							<td>
								<![endif]-->
									<table class="content" align="center" cellpadding="0" cellspacing="0" border="0">
										<tr>
											<td class="header" bgcolor="#ffffff">
												<table height="190" align="left" border="0" cellpadding="0" cellspacing="0">
													<tr>
														<td height="190" style="padding: 0 0 0 0;">
															<img src="cid:{banner}" width="100%" height="100%" border="0" alt="" />
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td class="innerpadding borderbottom">
												<table width="100%" border="0" cellspacing="0" cellpadding="0">
													<tr>
														<td class="h2">
															Witaj {receiverName},
														</td>
													</tr>
													<tr>
														<td class="bodycopy">
															Otrzymaliśmy prośbę zaminy hasła do Twojego konta w ClawLibrary.
														</td>
													</tr>
													<tr height="25"></tr>
													<tr>
														<td class="bodycopy">
															Jeżeli próbujesz ustawić nowe hasło kliknij w przycisk poniżej, a jeśli to nie Ty próbujesz je zmienić, koniecznie skontaktuj się z nami pod adresem {contactEmail}.
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td class="innerpadding borderbottom">
												<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">  
													<tr>
														<td align="center">
															<table class="buttonwrapper" bgcolor="#e05443" border="0" cellspacing="0" cellpadding="0">
																<tr>
																	<td class="button" height="45">
																		<a href="{baseUrl}/#!/password/set/{passwordResetKey}">Ustaw nowe hasło</a>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
									        <td class="innerpadding borderbottom">
									            <table width="100%" border="0" cellspacing="0" cellpadding="0">
									                <tr>
									                    <td class="bodycopy">
									                        Obsługa klienta,
									                    </td>
									                </tr>
									                <tr>
									                    <td class="bodycopy">
									                        W przypadku pytań, propozycji lub problemów prosimy o kontakt z naszym zespołem na adres kontakt@clawlibrary.com.
									                    </td>
									                </tr>
									                <tr height="25"></tr>
									                <tr>
									                    <td class="bodycopy">
									                        Serdecznie pozdrawaimy,
									                    </td>
									                </tr>
									                <tr>
									                    <td class="bodycopy">
									                        Twój zespół ClawLibrary
									                    </td>
									                </tr>
									            </table>
									        </td>
									    </tr>
										<tr>
											<td class="footer" >
												<table width="100%" border="0" cellspacing="0" cellpadding="0">
													<tr>
														<td align="center" class="footercopy">
															Wysłane przez ClawLibrary, ul. Sienkiewicza 82/84 | 90-318 Łódź<br/>
														    kontakt@clawlibrary.com
														</td>
													</tr>
													<tr>
														<td align="center" style="padding: 20px 0 0 0;">
															<table border="0" cellspacing="0" cellpadding="0">
																<tr>
																	<td width="37" style="text-align: center; padding: 0 10px 0 10px;">
																		<a href="http://www.facebook.com/">
																			<img src="cid:{facebook}" width="37" height="37" alt="Facebook" border="0" />
																		</a>
																	</td>
																	<td width="37" style="text-align: center; padding: 0 10px 0 10px;">
																		<a href="http://www.twitter.com/">
																			<img src="cid:{twitter}" width="37" height="37" alt="Twitter" border="0" />
																		</a>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</td>
										</tr>	
									</table>
								<!--[if (gte mso 9)|(IE)]>
							</td>
						</tr>
					</table>
				<![endif]-->
                </td>
            </tr>
        </table>
    </body>
</html>')