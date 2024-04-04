<?php

include_once ('procedures.php');
/*
ACTIONS FUNCION
ohter : deja libre la conversacion
option : muestra opciones
funtion : genera la ejecucion de una funcion
*/

class ChatBotCore extends Procedures{
    private $CommantInit = '';
    private $message = '';
    private $action = '';
    private $configurateJson = [];
    private $responses = [];

    public function __construct( $configurateJson) {
        $this->responses = $configurateJson['CommantInit'];
        $this->responses = $configurateJson['message'];
        $this->responses = $configurateJson['action'];
    }

    public function defaultMessages() {
        $this->responses = [
            'CommantInit' => null,
            'message' => 'Buen día, ¿en qué puedo ayudarte?',
            'action' => 'other',
        ];
        return $this->responses;
    }

    public function InitConversation($action) {
        switch ($action) {
            case 'other':

                break;
            case 'option':

                break;
            case 'funtion':

                 break;
            default:
                $this->responses = $this->defaultMessages();
                break;
        }
    }
}

// Usage example:
$chatBot = new ChatBotCore();
$greetingResponse = $chatBot->getResponse('greeting');
echo $greetingResponse;

?>