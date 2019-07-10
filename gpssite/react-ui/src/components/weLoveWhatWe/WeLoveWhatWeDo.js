import React from "react";
import styled from "styled-components";
import { screenSize } from "../../constants/screenBreakpoints";
import H1 from "../ui/H1";
import H3 from "../ui/H3";
import { colors } from "../../constants/colors";
import Underline from "../ui/Underline";

const StyledContainer = styled.div`
  height: fit-content;
  background-color: #FFFFFF;

  .paraProjectSlide {
    color: #373737;
    font-family: "Segoe UI";
    font-size: 20px;
    font-weight: 400;
    margin: 1% 2%;
    width:100% ;
    
    padding: 0 1% 0 1%;
    text-align: center;
  }

  
  .text2{
      color:#373737;
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
      padding:3% 3% 3% 3%;
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
  }
  .imageColumn{
      display:flex;
      flex-direction:column;
      align-items:center;
      width:200px;
      color:#373737;
      font-size:1.2vw;
      min-width:120px;
      margin:2% 2%;
  }
  .imagesize{
      width:80px;
      height:80px;
      margin-bottom:5%;

      
  }
  .imageText{
      text-align:center;
      font-family:poppins;
      font-size:0.8vw;
      
  }
  
  .imageColumnTitle{
    display:flex;
    flex-direction:column;
    justify-content:center;
    color:#373737;
    font-size:19px;
    font-family:poppins;
    font-weight:600;
    height:30px;
    margin-bottom:20px;
    /* margin-bottom:10%; */
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
        .imageColumn{
            margin:5% 0%;
        }
  }

  @media screen and (max-width: 400px) {
      .imageColumn{
          height:fit-content;
      }
      
  }

`;


class WeLoveWhatWeDo extends React.Component {
    constructor() {
        super();
    }

    render() {
        return (
            <StyledContainer>
                <div className="divmaincolumn">


                    
                    <div className="divcenter text2">
                        <H1 >WE LOVE WHAT WE DO</H1>
                    </div>
                    <Underline
                        backgroundColor="#22cc88"
                        width="50px"
                        height="2px"
                        className="underline"
                    />
                    <div className="text21">
                        <div className="divcenter">
                            <div className="paraProjectSlide">
                                Our key specification
                            </div>
                        </div>
                    </div>

                    <div className="divcenterImages">

                        <div className="imageColumn">
                            

                                <img className="imagesize" src={require('../../static/Icon/code.png')} />


                            
                            <div className="imageColumnTitle" >
                                <span style={{textAlign:"center"}}>CODING</span>
                            </div>
                            <span className="imageText">We care about our code. We maintain coding convention and provide optimal,clean code </span>
                        </div>

                        <div className="imageColumn">
                            

                                <img className="imagesize" src={require('../../static/Icon/devices.png')} />
                            
                            <div className="imageColumnTitle" >
                                <span style={{textAlign:"center"}}>WEB DESIGN</span>
                            </div>
                            <span className="imageText">We always care about responsive design. Which will provide user a seamless experience over all the devices</span>
                        </div>

                        <div className="imageColumn">
                            

                                <img className="imagesize" src={require('../../static/Icon/pencil-case.png')} />
                           
                            <div className="imageColumnTitle" >
                                <span style={{textAlign:"center"}} >DIGITAL MARKETING</span>
                            </div>
                            <span className="imageText">We provide total branding marketing strategy and roll over towards the market with best component</span>
                        </div>

                        <div className="imageColumn">
        
                                <img  className="imagesize" src={require('../../static/Icon/smartphone.png')} />
                            
                            <div className="imageColumnTitle" >
                                <span style={{textAlign:"center"}}>MOBILE DEVELOPMENT</span>
                            </div>
                            <span className="imageText">We provide mobile development according to market trend and provide clean mobile apps for your business</span>
                        </div>


                    </div>
                </div>
            </StyledContainer>
        );
    }
}

export default WeLoveWhatWeDo;