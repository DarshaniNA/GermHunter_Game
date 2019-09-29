<?php

$Email = $_REQUEST{"Email"};
$Password = $_REQUEST{"Password"};

$Hostname = "localhost";
$DBName = "accounts";
$User = "root";
$Password = "root";

mysql_connect($Hostname, $User, $Password) or die("Can't connect to DB");
mysql_select_db($DBName) or die("Can't connect to DB");

if(!$Email || $Password){
    echo"Empty";
}
else{
	$SQL = "SELECT * FROM accounts WHERE Email = '" . $Email . "'";
	$Result = @mysql_query($SQL) or die("DB Error");
	$Total = mysql_num_rows($Result);
	if($Total == 0) {
		$insert = "INSERT INTO 'accounts' ('Email', 'Password') VALUES ('" . $Email . "',MD5('" . $Password . "'))";
		$SQL1 = mysql_query($insert);
		echo "Succesful";
	} else {
		echo "Already used";
	}
}

mysql_close();
?>