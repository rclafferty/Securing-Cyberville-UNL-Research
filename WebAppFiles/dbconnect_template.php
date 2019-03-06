<?php
	$dbuser = 'username';
	$dbpassword = 'password';
	$db = 'database';
	$dbhost = 'database.url';
	$dbport = 3306;

	$dblink = mysqli_init();
	$dbconnection = mysqli_connect($dbhost,$dbuser,$dbpassword,$db);
	if ($dbconnection)
	{
		//print("success\n");
	}
	else
	{
		die("Connection failed " . mysqli_error());
	}
?>