Demo project of rentpayment created by beau of mlfusion.
This project is only use for demo purpose only

#Prerequisites

Windows
Visual Studio 2013 or higher

Install database scheme
Open the Rent.Document folder and run the following sql scripts in SMMS
   - setup db scheme.sql
   - setup default user.sql
   
Running the Rent app
Open the solution Rent.Solution.sln and build solution

Setup out going emails
Open the Rent.Common project, open the app.config file. Update the applicationSettings section
            <setting name="Host" serializeAs="String">
                <value>host name</value>
            </setting>
            <setting name="Email" serializeAs="String">
                <value>add yours</value>
            </setting>
            <setting name="Password" serializeAs="String">
                <value>add yours</value>
            </setting>
            <setting name="MailHost" serializeAs="String">
                <value>add yours</value>
            </setting>
