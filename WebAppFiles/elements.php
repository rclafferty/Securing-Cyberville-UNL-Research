<?php
	error_reporting(E_ERROR | E_PARSE | E_NOTICE);
	include_once 'dbconnect.php';

	$query = "SELECT name FROM actions";
	$result = mysqli_query($dbconnection, $query);
	$action = "any";

	echo "<select name=\"actions\"><option value=\"any\">Any</option>";
	if ($result->num_rows > 0)
	{
		while($row = $result->fetch_assoc())
		{
	        echo "<option value=\"". $row["name"]. "\">" . $row["name"] . "</option>";
		}
	}
	echo "</select>";
	mysqli_close($dbconnection);
?>