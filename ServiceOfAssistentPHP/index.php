<?php
/**
 * version: 0.0.1
 * Autor: Ricardo J Moo Vargas
 * Fecha de inicio: 2 de abril de 2024
 * 
 */
// require_once "./core/FrontController.php";
// Crear una instancia de la clase FrontController
//$frontController = new FrontController();

// Obtén el contenido del archivo JSON
$json = file_get_contents('core/database/jsonProcesuresHola.json');

// Decodifica el JSON a un array asociativo de PHP
$data = json_decode($json, true);

// Ahora puedes acceder a los valores en el array
echo $data['CommantInit']; // Imprime: Opcion 1.1
echo '--';
echo $data['message']; // Imprime: esta opciones genera una accion
echo '--';
echo $data['action']; // Imprime: funtion
echo '--';


?>
