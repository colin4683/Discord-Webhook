<?php 
require_once __DIR__ . '/vendor/autoload.php';
use \DiscordWebhooks\Client;
use \DiscordWebhooks\Embed;

$webUrl = $_GET['webUrl'];

$name = $_GET['botName'];

$avatarurl = $_GET['avatar'];

$webhook = new Client($webUrl);
$embed = new Embed();



// variables
$embedDescription = $_GET['embedDescription'];
$embedTitle = $_GET['embedTitle'];
$message = $_GET['message'];
$embedColor = $_GET['embedColor'];
$embedImage = $_GET['embedImage'];

if(isset($_GET['embed']))
{
$embed->title($embedTitle);
$embed->description($embedDescription);
$embed->color($embedColor);
$webhook->avatar($avatarurl)->username($name)->message($message)->embed($embed)->send();
}



if(isset($_GET['regular']))
{
	$webhook->avatar($avatarurl)->username($name)->message($message)->send();
}
if(isset($_GET['picture']))
{
	$embed->image($embedImage);
	$webhook->avatar($avatarurl)->username($name)->message($message)->embed($embed)->send();
}
 ?>
