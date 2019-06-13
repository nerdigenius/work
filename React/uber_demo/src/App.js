import React from 'react';
import styled from 'styled-components';
import logo from './earn_2x.webp'


const StyledContainer = styled.div`
.header{
  display:flex;
  justify-content:space-evenly;
  background:black;
  color:white;
  padding:1% 0;
  align-items:center
}
.header1{
  display:flex;
  padding-left:10px;
}
.header2{
  display:flex;
}
.textlogo{
  display:flex;
  align-items:center;
}
.text2{
  padding-right:10px;
  font-weight:;
}
.text{
  font-size:25px
}
.imagesection{
  display:flex;
  text-align:center;
  position:relative;
  justify-content:center;

}
.otheroption{
  position:absolute;
}

`


function App() {
  return (
    <StyledContainer>
      <div className="header">
        <div className="textlogo">
          <div className="logo">
            <div className="text">Uber</div>
          </div>

          <div className="header1">
            <div className="text2">Our Products</div>
            <div className="text2">Our Company</div>
            <div className="text2">Safety</div>
            <div className="text2">Help</div>
          </div>

        </div>

        <div className="header2" >
          <div className="text2">EN</div>
          <div className="text2">Rewards</div>
          <div className="text2">Pay</div>
          <div className="text2">Login</div>
          <div className="text2">SignUp
          </div>

        </div>






      </div>
      <div className="imagesection">
      <div className="otheroption">
          <div>earn</div>
          <div>ride</div>
          <div>eat</div>
          <div>freight</div>
          <div>business</div>
          <div>transit</div>
          <div>bike</div>
          <div>fly</div>
        </div>
        
        <img src={logo} className></img>
      </div>
    </StyledContainer>

  );
}

export default App;
