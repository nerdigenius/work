import React, {Component} from 'react';
import styled from 'styled-components';
import logo from './earn_2x.webp'
import earnFilled from './Earn-filled.svg';
import ride from './ride.svg';
import eat from './eat.svg';
import freight from './freight.svg';
import business from './business.svg';
import transit from './transit.svg';
import bike from './bike.svg';
import fly from './fly.svg';
import sidebaricon from './srcsidebaricon1.png';


const StyledContainer = styled.div`
.header{
  display:flex;
  justify-content:space-evenly;
  background:black;
  color:white;
  height:80px;
  align-items:center
}
.header1{
  display:flex;
  padding-left:10px;
}
.header2{
  display:flex;
  height:100%;
  align-items:center;
}
.textlogo{
  display:flex;
  align-items:center;
}
.text2{
  display:flex;
  justify-content:center;
  padding-right:10px;
  /* font-weight: */
}
.text3{
  display:flex;
  justify-content:center;
  padding-right:10px;
  /* font-weight: */
}
.text{
  font-size:25px
}
.imagesection{
  display:flex;
  text-align:center;
  position:relative;
  justify-content:center;
  margin:0 16%

}
.otheroption{
  position:absolute;
  display:flex;
  justify-content:center;
  width:80%;
  margin:5% 0 0 0;


  }
.innertabs{
  display:flex;
  flex-direction:column;
  justify-content:space-around;
  width:stretch;
  background-color:white;
  height:75px;
  padding:2%;
  font-weight:bold;
  
}
.innertabs:hover{
  color:#2C71F1;
}
.image{
  width:100%;
}
.signup{
  display:flex;
  background-color:#2C71F1;
  
}
.signupspan{
  margin:9px;
  font-size:15px;
  font-weight:500;
}
.header2svg{
  display:flex;
  flex-direction:column;
  justify-content:center;
  
}
.header2text{
  font-size:14px;
  font-weight:bold;
  margin-left:6px;
}
.sidebarmenu{
  display:none;
}
@media screen and (max-width: 960px) {
  .text2 ,.header1{
    display:none;
  }
  .header{
    justify-content:space-between;
    padding:0 2%;
  }
  .sidebarmenu{
  display:flex;
}

.signup{
  margin-right:10px;
}

.imagesection{
  margin:0;
}
  
}


`

class App extends Component{

  render () {
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
            <div className="text2">
              <div className="header2svg">
                <svg width="1em" height="1em" viewBox="0 0 24 24" fill="none"><title>EN</title><path d="M12 1C5.9 1 1 5.9 1 12s4.9 11 11 11 11-4.9 11-11S18.1 1 12 1zm8 11c0 .7-.1 1.4-.3 2-.6-1.5-1.6-3.1-3-4.7l1.8-1.8c1 1.3 1.5 2.8 1.5 4.5zM6.5 6.5c1.3 0 3.6.8 6 2.9l-3.2 3.2C7.1 9.8 6.5 7.5 6.5 6.5zm8.1 5c2.3 2.7 2.9 5 2.9 6-1.3 0-3.6-.8-6-2.9l3.1-3.1zm1.9-6.1l-1.9 1.9c-1.6-1.4-3.2-2.4-4.7-3 .7-.2 1.3-.3 2-.3 1.8 0 3.3.5 4.6 1.4zM4 12c0-.7.1-1.4.3-2 .6 1.5 1.6 3.1 3 4.7l-1.8 1.8C4.5 15.2 4 13.7 4 12zm3.5 6.6l1.9-1.9c1.6 1.4 3.2 2.4 4.7 3-.7.2-1.3.3-2 .3-1.8 0-3.3-.5-4.6-1.4z" fill="currentColor"></path></svg>
              </div>
              <span className="header2text">EN</span>
            </div>
            <div className="text2">
              <div className="header2svg">
                <svg width="1em" height="1em" viewBox="0 0 24 24" fill="none"><title>Rewards</title><path fill-rule="evenodd" clip-rule="evenodd" d="M19.3 12L12 4.7 4.7 12H12v7.3l7.3-7.3zM.5 12L12 .5 23.5 12 12 23.5.5 12z" fill="currentColor"></path></svg>
              </div>
              <span className="header2text">Rewards</span>
            </div>
            <div className="text2">
              <div className="header2svg">
                <svg width="1em" height="1em" viewBox="0 0 24 24" fill="none"><title>Pay</title><path fill-rule="evenodd" clip-rule="evenodd" d="M1 20V4h22v16H1zm16-6h3V7H7v3H4v7h13v-3zm-2-2a3 3 0 1 1-6 0 3 3 0 0 1 6 0z" fill="currentColor"></path></svg>
              </div>
              <span className="header2text">Pay</span>
            </div>
            <div className="text3">
              <div className="header2svg">
                <svg width="1em" height="1em" viewBox="0 0 24 24" fill="none"><title>Log in</title><path fill-rule="evenodd" clip-rule="evenodd" d="M17.5 6.5c0 3-2.5 5.5-5.5 5.5S6.5 9.5 6.5 6.5 9 1 12 1s5.5 2.5 5.5 5.5zm-3 0C14.5 5.1 13.4 4 12 4S9.5 5.1 9.5 6.5 10.6 9 12 9s2.5-1.1 2.5-2.5zM3 20c0-3.3 2.7-6 6-6h6c3.3 0 6 2.7 6 6v3h-3v-3c0-1.7-1.4-3-3-3H9c-1.6 0-3 1.3-3 3v3H3v-3z" fill="currentColor"></path></svg>
              </div>
              <span className="header2text">Login</span>
            </div>
            <div className="signup">
              <span className="signupspan">Sign up</span>
            </div>
            <div className="sidebarmenu">
              <img src={sidebaricon}></img>
            </div>
  
          </div>
  
  
  
  
  
  
        </div>
        <div className="imagesection">
          <div className="otheroption">
            <div className="innertabs">
              <img src={earnFilled}></img>
              Earn
            </div>
            <div className="innertabs">
              <img src={ride}></img>
              Ride
            </div>
            <div className="innertabs">
              <img src={eat}></img>
              Eat
            </div>
            <div className="innertabs">
              <img src={freight}></img>
              Freight
            </div>
            <div className="innertabs">
              <img src={business}></img>
              Business
            </div>
            <div className="innertabs">
              <img src={transit}></img>
              Transit
            </div>
            <div className="innertabs">
              <img src={bike}></img>
              Bike
            </div>
            <div className="innertabs">
              <img src={fly}></img>
              Fly
            </div>
          </div>
  
          <img src={logo} className="image"></img>
        </div>
      </StyledContainer>
  
    );
  }

}




export default App;
