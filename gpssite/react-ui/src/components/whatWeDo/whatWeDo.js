import React from "react";
import styled from "styled-components";
import { screenSize } from "../../constants/screenBreakpoints";
import H1 from "../ui/H1";
import H3 from "../ui/H3";
import { colors } from "../../constants/colors";
import Underline from "../ui/Underline";

const StyledContainer = styled.div`
  height: auto;
  background-color: #01C476;

  .paraProjectSlide {
    color: #fff;
    font-family: "Segoe UI";
    font-size: 20px;
    font-weight: 400;
    margin-left: 2%;
    margin-right: 2%;
    padding: 0 1% 0 1%;
    text-align: center;
    width:100%;
  }

  .textWhatWeDo{
      /* font-size:1.3vw;
      color:white;
      font-family:poppins;
      font-weight:bold;
      margin:2%; */
      
  }
  .text2{
      /* font-size:5vw;
      color:white;
      font-family:poppins;
      font-weight:bold; */
      margin-bottom:30px;
      margin-top:30px;
  }
  .text21{
      font-size:2vw;
      color:white;
      font-family:poppins;
      margin-bottom:3%;
  }
  .text3{
      font-size:1.4vw;
      color:white;
  }
  .divmaincolumn{
      display:flex;
      flex-direction:column;
      justify-content:space-around;
      height:100%;
      padding:3% 0% 3% 0%;
  }
  
  .header.divcenter{
      margin-bottom: 20px;
  }
  
  .divcenter{
      display:flex;
      justify-content:center;
  }
  

  .divcenterImages{
    display:flex;
    justify-content:space-evenly;
    flex-wrap:wrap;
    width:100%;
  }
  .imageColumn{
      display:flex;
      flex-direction:column;
      align-items:center;
      flex-wrap:wrap;
      width:11%;
      color:white;
      font-size:1.2vw;
      min-width:200px;
      margin:2% 2%;
      padding:2% 1%;
      height: fit-content;
      
  }
  .imagesize{
      display:flex;
      margin-bottom:5%;
      justify-content:center;

      height:127px;
      width:127px;
  }
  .imagesize2{
      display:none;
      margin-bottom:5%;
      justify-content:center;

      height:127px;
      width:127px;
  }
  .imageText{
      text-align:center;
      font-family:poppins;
      font-size:0.8vw;
      
  }
  .imageColumnTitle{
    text-align:center;
    color:white;
    font-size:18px;
    font-family:poppins;
    font-weight:bold;
    height:25px;
    margin-bottom:30px;
    /* margin-bottom:10%; */
  }
  .divcenterImages{
    .imageColumn:hover{
      background-color:white;
      color:${colors.primaryGreen};
        .imagesize{
        display:none;
    }
    .imagesize2{
        display:flex;
    }
    .imageColumnTitle{
        color:${colors.primaryGreen};
    }
    }
    
  

  }
  
  
  
 
  @media  screen and (max-width: 992px){
    .imageText{
        font-size:1.2vw;
    }
    .imageColumnTitle{
        font-size:1.8vw;
        
    }
      
  }
  @media screen and (max-width: 700px) {
      .textWhatWeDo{
        /* font-size:22px;
        text-align:center; */
      }
      .imageText {
            font-size: 14px;
        }
        .imageColumnTitle {
            font-size: 16px;
        }
        .text3 {
            font-size: 16px;
            text-align:center;
        }
        .text2{
            /* font-size:18px; */
            text-align:center;
        }
  }

`;


class WhatWeDo extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <StyledContainer>
                <div className="divmaincolumn">


                    <div className="header divcenter">
                        {/* <span className="textWhatWeDo">WHAT WE DO</span> */}
                        <H3 className='title' color="#fff" center uppercase>What we do</H3>
                    </div>

                    <div className="divcenter text2">
                        <H1 color="#fff">WE SHIP SUCCESS EVERYDAY</H1>
                    </div>
                    <div className="text21">
                        <div className="divcenter">
                            {/* <span className="text3">Our main focus is to make the User Experience very simple and easy</span> */}
                            <div className="paraProjectSlide">
                                Our main focus is to make the User Experience very simple and
                                easy.
                            </div>
                        </div>
                        <div className="divcenter">
                            {/* <span className="text3">Simplicity is our strength</span> */}
                            <div className="paraProjectSlide">
                                Simplicity is our strength
                            </div>
                        </div>
                    </div>

                    <div className="divcenterImages">

                        <div className="imageColumn">


                            <img className="imagesize" src={require('../../static/Icon/web-programming.png')} />
                            <img className="imagesize2" src={require('../../static/Icon/web-programming-green.png')} />


                            <div className="imageColumnTitle" >
                                <span >CODING</span>
                            </div>
                            <span className="imageText">We try to do efficient coding for you and make your dream project successful</span>
                        </div>

                        <div className="imageColumn">


                            <img className="imagesize" src={require('../../static/Icon/computer.png')} />
                            <img className="imagesize2" src={require('../../static/Icon/computer_green.png')} />

                            <div className="imageColumnTitle" >
                                <span >WEB DESIGN</span>
                            </div>
                            <span className="imageText">We try to do efficient coding for you and make your dream project successful</span>
                        </div>

                        <div className="imageColumn">


                            <img className="imagesize" src={require('../../static/Icon/pencil.png')} />
                            <img className="imagesize2" src={require('../../static/Icon/pencil_green.png')} />

                            <div className="imageColumnTitle" >
                                <span >DIGITAL MARKETING</span>
                            </div>
                            <span className="imageText">We try to do efficient coding for you and make your dream project successful</span>
                        </div>

                        <div className="imageColumn">

                            <img className="imagesize" src={require('../../static/Icon/mobile_development.png')} />
                            <img className="imagesize2" src={require('../../static/Icon/mobile_development_green.png')} />

                            <div className="imageColumnTitle" >
                                <span >MOBILE DEVELOPMENT</span>
                            </div>
                            <span className="imageText">We try to do efficient coding for you and make your dream project successful</span>
                        </div>


                    </div>
                </div>
            </StyledContainer>
        );
    }
}

export default WhatWeDo;