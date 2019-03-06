<?php
	error_reporting(E_ERROR | E_PARSE | E_NOTICE);
	include_once 'dbconnect.php';
	//include_once 'elements.php';
	$query = "SELECT u.user, u.date, a.name as 'action', u.sceneName, u.comment FROM userLog u JOIN actions a ON a.id = u.action";
	
	$user = "Player";
	$sort = " ORDER BY u.date ASC";
	if (!empty($_GET))
	{
		$user = $_GET['user'];
		$query = $query . " WHERE user LIKE '$user'";
	}
	/*if ($action != "any")
	{
		$query = $query . " WHERE action LIKE '$action'";
	}*/

	$query = $query . " $sort";

	//echo "<p>$query</p>";
	$result = mysqli_query($dbconnection, $query);
	if ($result->num_rows > 0)
	{
		// output data of each row
		echo "<table>";
		echo "<tr>";
		echo "<th>User</th>";
		echo "<th>Date</th>";
		echo "<th>Action</th>";
		echo "<th>Scene</th>";
		echo "<th>Comment</th>";
		echo "</tr>";

		while($row = $result->fetch_assoc()) {
	        echo "<tr><td>" . $row["user"] . "</td><td>" . $row["date"] . "</td><td>" . $row["action"] . "</td><td>" .$row["sceneName"] . "</td><td>" . $row["comment"] . "</td></tr>";
		}
	}
	else
	{
		$query = "SELECT u.user, u.date, a.name as 'action', u.sceneName, u.comment FROM userLog u JOIN actions a ON a.id = u.action";
		$result = mysqli_query($dbconnection, $query);
		if ($result->num_rows > 0)
		{
			// output data of each row
			echo "<table>";
			echo "<tr>";
			echo "<th>User</th>";
			echo "<th>Date</th>";
			echo "<th>Action</th>";
			echo "<th>Scene</th>";
			echo "<th>Comment</th>";
			echo "</tr>";

			while($row = $result->fetch_assoc()) {
		        echo "<tr><td>" . $row["user"] . "</td><td>" . $row["date"] . "</td><td>" . $row["action"] . "</td><td>" .$row["sceneName"] . "</td><td>" . $row["comment"] . "</td></tr>";
			}
		}
		else
		{
			echo "<p>Select a user</p>";
		}
	}
	mysqli_close($dbconnection);
?>
