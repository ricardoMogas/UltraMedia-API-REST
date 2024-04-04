<?php
// Preparar los datos para enviar el mensaje a través de Facebook Graph API
    $url = 'https://graph.facebook.com/v18.0/258589487342464/messages';
    $headers = array(
        'Authorization: Bearer EAADvWXT33qQBO3ZBUkvTEo5V2OVzVYmx8vQHXFx5NuvR3ztuSDjo8P72bAOl3X18FsKdv45bz2BEZBdEiinrXQiAfFDLgkPriu4Cw7PdppGV2OM7PPle38OvMBcz37b4xSAVqvdLx90G6tLWSzyHMxGvx8P0EIyv6rU0JcZBwkSiordPByfTlpgLDwc7MOzEvFn728OAFfma3647pAZD',
        'Content-Type: application/json'
    );
    $data = array(
        'messaging_product' => 'whatsapp',
        'to' => '529815937333',
        'type' => 'template',
        'template' => array(
            'name' => 'hello_world',
            'language' => array(
                'code' => 'en_US'
            )
        )
    );

    // Realizar la solicitud POST utilizando cURL
    $ch = curl_init($url);
    curl_setopt($ch, CURLOPT_POST, true);
    curl_setopt($ch, CURLOPT_POSTFIELDS, json_encode($data));
    curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
    $response = curl_exec($ch);
    curl_close($ch);

    // Verificar la respuesta
    if ($response) {
        // La solicitud fue exitosa, puedes procesar la respuesta aquí
        $response = json_decode($response, true);
        // ...
    } else {
        // La solicitud falló, maneja el error aquí
        // ...
    }