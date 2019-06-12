import React from 'react';
import './App.css';
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
