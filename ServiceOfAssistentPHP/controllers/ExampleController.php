<?php
require_once(__DIR__ . "/../core/responseData.class.php");

class ExampleController extends responseData{
    public function index(...$params) 
    {
        /** 
         * SI NO EXISTE EL doGet, doPost, doDelete y doPut
         * ENTONCES SE EJECUTA EL INDEX COMO METODO PRESETERMINADO
        **/
    }
    
    public function doGet(...$params) 
    {
        // 
        /*
        PARA EVITAR EL ERROR DE QUE NO EXISTA EL PARAMETRO NAME
        if (!isset($params["name"])) {
            $responseError = parent::error_400();
            echo json_encode($responseError);
            return;
        }
        */

        $data = array(
            "name" => "Ricardo",
            "data" => [
                array(
                    "id" => 1,
                    "name" => "Ricardo",
                    "password" => "1234"
                ),
                array(
                    "id" => 2,
                    "name" => "Maria",
                    "password" => "5678"
                ),
                array(
                    "id" => 3,
                    "name" => "Juan",
                    "password" => "abcd"
                ),
                array(
                    "id" => 4,
                    "name" => "Ana",
                    "password" => "efgh"
                )
            ]
        );
        if (isset($data)) {
            return $data;
        } else {
            $responseError = parent::error_404();
            return $responseError;
        }
    }

    public function doPost(...$params) {
        $body = $params["data"];
        return $body["name"];
        /* 
        ENTPOINT
        http://localhost/PFI-Services-Api/Example
        BODY
        {
            "message" : [1, 2, 3],
            "data" : {
                "name" : "Ricardo",
                "lastName" : "Moo"
            }
        }
        RESPUESTA
        Ricardo
        */
    }
}
?>