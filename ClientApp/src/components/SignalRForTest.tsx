import React from 'react';
import * as signalR from "@microsoft/signalr";
import { Button, Label } from 'reactstrap';
import { connect } from 'react-redux';


interface SignalRState {
    message: string,
    hubConnection: any
}

class SignalRForTest extends React.Component<{}, SignalRState>{
  constructor(props: any) {
    super(props);
    
    this.state = {
      message: '',
      hubConnection: null
    };

    this.handleSendClick = this.handleSendClick.bind(this);
    this.handleGetSheduleClick = this.handleGetSheduleClick.bind(this);
  }

  componentDidMount(){
    /********************************/                    
    //подключение без аутентификационного токена (JWT)
    /********************************/  
    const connection = new signalR.HubConnectionBuilder()
                        .withUrl("/washme")
                        .build();

    /********************************/                    
    //подключение с использованием аутентификационного токена (JWT)
    /********************************/           
    const connectionWithToken = new signalR.HubConnectionBuilder()
                        .withUrl("/washme", {accessTokenFactory: () => "[Token Value]"})
                        .build();

    this.setState({hubConnection: connection});
    
    /********************************/                    
    //эти методы может вызывать сервер
    /********************************/
    //подписка на метод InitializationResponse
    connection.on("InitializationResponse", (msg: string) => {
        this.setState({
            message: msg
        });
    })
    //подписка на метод InitializationResponse
    connection.on("Notify", (msg: string) => {
      this.setState({
          message: msg
      });
    });


    /********************************/                    
    //подключение к серверу
    /********************************/
    connection.start().then( ()=>{
      
    });
  }

    /********************************/                    
    //это клиент вызывает методы сервера
    /********************************/
  handleSendClick() {
    if (this.state.hubConnection !== null)
      this.state.hubConnection.send("Send", "Hello world Send Method");
  }

  handleGetSheduleClick() {
    if (this.state.hubConnection !== null)
      this.state.hubConnection.invoke("GetShedule", "Hello world Shedule Method");
  }

  render() {
    return(
     <div>
        <Label>Hello world</Label>
        <Label title="qwerty">{this.state.message}</Label>
        <Button onClick={this.handleSendClick}>Send</Button>
        <Button onClick={this.handleGetSheduleClick}>GetShedule</Button>

        
    </div>);
  }
}

export default SignalRForTest;