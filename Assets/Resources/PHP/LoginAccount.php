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
	$SQL = "SELECT * FROM accounts WHERE Email" '" . $Email . "'";
	$Result_id = @mysql_query($SQL) or die("DB Error");
	$Total = mysql_num_rows($Result_id);
	if($Total) {
		$datas = @mysql_fetch_array($Result_id);
		if (!strcmp($Password, $datas["Password"])) {
			$sql2 = "SELECT Characters FROM accounts WHERE Email = '" . $Email . "'";
			$Result_id2 = @mysql_query($sql2) or die("DB Error");
			while($row = mysql_fetch_array($Result_id2))
			{
				echo $row["Characters"];
				echo":";
				echo"Succesful";
			}
		}
	} else {
		echo "Wrong Password";
} else{
	echo"Email does not exist";
}
}

mysql_close();
?>