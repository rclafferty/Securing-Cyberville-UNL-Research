<?php
	error_reporting(E_ERROR | E_PARSE | E_NOTICE);
	include_once 'dbconnect.php';

	$user = $_POST['user'];
	$user = strip_tags($user);
	$date = $_POST['date'];
	$date = strip_tags($date);
	$comment = $_POST['comment'];
	$comment = strip_tags($comment);
	$action = $_POST['action'];
	$action = strip_tags($action);
	$scene = $_POST['scene'];
	$scene = strip_tags($scene);

	$query = "INSERT INTO userLog (user, date, action, sceneName, comment) VALUES ( '".$user."', '".$date."', '".$action."', '".$scene."', '".$comment."' )";
	$result = mysqli_query($dbconnection, $query);
	$row = mysqli_fetch_row($result);
	echo $row . "\n";
	if ($row)
	{
		//Returns an error
		$dataArray = array('success' => false, 'error' => 'ERROR WHILE INSERTING LOG');
	}
	else
	{
		//INSERTS DON'T RETURN ANYTHING STUPID!!!!!
		$dataArray = array('success' => true, 'error' => 'none');
	}

	echo json_encode($dataArray);
	mysqli_close($dbconnection);
?>