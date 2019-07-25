<?php 
require_once __DIR__ . '/vendor/autoload.php';
use \DiscordWebhooks\Client;
use \DiscordWebhooks\Embed;

$a = "https://discordapp.com/api/webhooks/603718705540759552/lh9Mn8RwO5M4OUaAANvV-E6xW7qUHICp16UoBEs3sW5yFF-jK80Ferb2nbPLYXMsb7tz";
$webUrl = $_GET['webUrl'];

$name = $_GET['botName'];

$af = "https://cdn.shopify.com/s/files/1/1227/1058/collections/90302cce-3a00-4b99-b9ed-c390b3e97a34_2048x2048.png?v=1529797212";
$avatarurl = $_GET['avatar'];

$webhook = new Client($webUrl);
$embed = new Embed();



// variables
$embedDescription = $_GET['embedDescription'];
$embedTitle = $_GET['embedTitle'];
$message = $_GET['message'];
$embedColor = $_GET['embedColor'];


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

 ?>