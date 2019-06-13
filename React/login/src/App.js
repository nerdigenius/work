import React from 'react';
//import './App.css';
import logo from './logo.png'
import styled from 'styled-components';

const StyledContainer = styled.div`
  .row {
    margin-top: 1.5%;
    margin: 1.5% 10% 20px 10%;
    display: flex;
    flex-direction: row;
    label {
      width: 25%;
      margin-right: 20px;
    }
  }
  .loginbtn {
    .button {
      &:hover {
        background: black;
        color: white;
      }
    }
  }
  .loginbox{
  position: relative;
  width: 30%;
  height: 70%;
  background: white;
  top: 50vh;
  left: 50%;
  transform: translate(-50%, -50%);
  padding: 50px;
}
.avatar{
  width: 150px;
  /* height:30%; */
  border-radius:100%; 
  position:absolute ;
  top: -21%;
  left: 50%;
  transform: translate(-50%, 0);
}

h1{
  text-align: center;
}
.textinput{
  width: 80%;
  outline: none;
  border: 1px solid grey;
  padding: 5px;
}

.loginbtn{
  margin-top: 5%;
  text-align: center;
  height: 100px;
}
.button {
  padding: 5px 10px;
  border: 1px solid #aeaeae;
  background: white;
  color: black;
  transition: all 0.3s ease-out;
}
@media screen and (max-width: 960px) {
  .row {
    flex-direction: column;
    align-items: center;
  }
  .loginbox{
    width: 90%;
  }
  .avatar {
    top: -14%;
    width: 100px;
  }
  .textinput{
    width: 100%;
  }
  label {
    width: 100%;
    text-align: center;
    margin: 10px 0;
  }
}

`;

function App() {
  return (
    <StyledContainer>
      <div className="loginbox" >
      <img src={logo} alt="Logo" className="avatar" />
      <form className="inputform">
        <h1>Log In</h1>
        <div className="row">
          <label>Username</label>
          <input className="textinput" type='text' name="" placeholder="Enter Username" ></input>
        </div>
        <div className="row">
          <label>Password</label>
          <input className="textinput" type='password' name="" placeholder="Enter Password" ></input>
        </div>
        <div className="loginbtn">
          <input className='button' type="submit" value="Login"></input>
        </div>
      </form>
    </div>
    </StyledContainer>
    
  );
}

export default App;
