# Secret Santa Assigner

----
*Welcome to the secret santa assigner console application.*

In order to assign and send secret santas, you will need to define the following environment variables:

* `EMAIL_SENDER_HOST`: The host of your mail server.
* `EMAIL_SENDER_SERVICE_ADDRESS`: The email address used to send the assignments.
* `EMAIL_SENDER_DOMAIN`: The domain of the emails to be used.

Please note that this Secret Santa Assigner was built to send emails from the `EMAIL_SENDER_HOST` via SMTP using `EMAIL_SENDER_SERVICE_ADDRESS` as the sending account.
Additionally, it assumes that each participant has a valid email account on the `EMAIL_SENDER_DOMAIN` constructed by concatenating their first name, a period, and their last name.

Please use the following arguments to assign and send secret santas:

`-participants <file path>`: the path to the .txt file that holds the names of the participants in your Secret Santa Event. Ensure that each line in this file is its own participant; ensure that each participant includes a valid first and last name separated by one space.

`-email <file path>`: the path to the .txt file that includes the body of the email to be sent. This application will replace {SECRET_SANTA} with the secret santa participant and {RECIPIENT} with their recipient.

`-print`: include this if you would like to see the printed assignments.

`-send`: include this if you would like to send the assignments to the included participants.

`-help`: display this menu.