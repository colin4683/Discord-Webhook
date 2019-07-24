<?php 
$curl = curl_init("https://discordapp.com/api/webhooks/603718705540759552/lh9Mn8RwO5M4OUaAANvV-E6xW7qUHICp16UoBEs3sW5yFF-jK80Ferb2nbPLYXMsb7tz"); 
curl_setopt($curl, CURLOPT_POST, 1);
curl_setopt($curl, CURLOPT_POSTFIELDS, json_encode(array("content" => "hello")));

echo curl_exec($curl);
 ?>