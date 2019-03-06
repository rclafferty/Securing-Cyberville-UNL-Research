<!DOCTYPE HTML>
<html>
	<head>
		<title>Report - Securing Cyberville</title>
		<link href="styles/secure.css" rel="stylesheet" type="text/css">
		<meta name="viewport" content="width=device-width, initial-scale=1.0">
		<meta charset="UTF-8">
	</head>
	<body>
		<h1>Securing Cyberville Report Website</h1>
		<p><a href="ResearchBuild1.zip" target="_self">Download the game</a></p>
		<?php
			error_reporting(E_ERROR | E_PARSE | E_NOTICE);
			include_once 'dbconnect.php';

			echo "<form action=\"index.php\">
				User: <input type=\"text\" name=\"user\" value=\"\"><br>
				Action: <select name=\"action\">
					<option value=\"any\">Any</option>";
					
					$query = "SELECT name FROM actions";
					$result = mysqli_query($dbconnection, $query);
					if ($result->num_rows > 0)
					{
						while($row = $result->fetch_assoc())
						{
					        echo "<option value=\"". $row["name"]. "\">" . $row["name"] . "</option>";
						}
					}
			echo "</select>
				<br>
				Sort By: 
				<select name=\"sort\">
					<option value=\"user\">User</option>
					<option value=\"date\">Date</option>
					<option value=\"action\">Action</option>
					<option value=\"sceneName\">Scene</option>
					<option value=\"comment\">Comment</option>
				</select>

				<select name=\"order\">
					<option value=\"asc\">Ascending</option>
					<option value=\"desc\">Descending</option>
				</select>
  				<input type=\"submit\" value=\"Submit\">
			</form>";

			echo "<h2>Results:</h2>";

			$user = "";
			$sort = "u.date";
			$order = "ASC";
			$action = "any";

			if (!empty($_GET))
			{
				$user = $_GET['user'];
				$sort = $_GET['sort'];
				$order = $_GET['order'];
				$action = $_GET['action'];
			}

			$query = "SELECT u.user, u.date, a.name as 'action', u.sceneName, u.comment FROM userLog u JOIN actions a ON a.id = u.action ";
			if ($action <> "any" && $user <> "")
			{
				$query = $query . " WHERE u.user LIKE '$user' AND a.name LIKE '$action' ";
			}
			else if ($action <> "any")
			{
				$query = $query . " WHERE a.name LIKE '$action' ";
			}
			else if ($user <> "")
			{
				$query = $query . " WHERE u.user LIKE '$user' ";
			}

			$query = $query . " ORDER BY $sort $order";

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
				echo "<p>No Results Available</p>";
			}

			mysqli_close($dbconnection);
		?>

	
	</body>
</html>
