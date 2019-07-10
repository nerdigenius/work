import React from "react";
import styled from "styled-components";
import { screenSize } from "../../constants/screenBreakpoints";
import INPUT from "../ui/Input";
import { colors } from "../../constants/colors";
import H3 from "../ui/H3";
import Underline from "../ui/Underline";

const StyledContainer = styled.div`
  height: auto;
  background-color: ${colors.primaryGreen} ;

  .header.divcenter{
      /* margin-bottom: 20px; */
  }
  
  .divcenter{
      display:flex;
      justify-content:center;
  }

  .title{
      color:white;
      font-family: Roboto;
      font-size: 16px;
      width:100%
  }
  

  .maincontent{
      display:flex;
      justify-content:space-evenly;
      height:90%; 
      padding: 2% 0% 5% 9%;
      

  }
  .inputsection{
      display:flex;
      flex-direction:column;
      width:50%;
      height:100%;
      
      
  }
  .buttonsection{
      display:flex;
      flex-direction:column;
      width:50%;
      align-items:left;
      padding-left:3%
      
  }
  .inputdiv{
      display:flex;
      align-items:flex-end;
      width:100%;
      height:25%;
  }
  .row {
    display: flex;
    flex-direction: row;
    align-items:center;
    width:100%;
    margin-bottom:4%;
    color:white;
    label {
        display:flex;
        justify-content:flex-end;
        width: 25%;
        font-size:1vw;
        padding:2%;
        font-family: Roboto;
        font-size: 14px;
      /* margin-right: 20px; */
    }
    
  }
  .rowDescription {
    display: flex;
    flex-direction: row;
    width:100%;
    margin-bottom:5%;
    color:white;
    label {
        display:flex;
        justify-content:flex-end;
        width: 25%;
        font-size:1vw;
        padding-right:2%;
        font-family: Roboto;
        font-size: 14px;
      /* margin-right: 20px; */
    }
    
  }
  .textarea{
       background:${colors.primaryGreen};
       resize:none;
       width:75%;
       border:1px solid white;
       border-radius:4px;
       padding:5px 5px;
       color:white;
       font-size:16px;
       outline:none;
       font-family:-apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif;
    }
  .textinput{
        width:75%;
        color:white;
        font-size:16px;
        outline:none;

    }
  .submitbtn{
      display:flex;
      justify-content:flex-start;
      margin-left:25%;
    
  }
  
  .inputform{
      display:flex;
      flex-direction:column;
      height:100%;
      justify-content:space-between;
      border-right:1px solid white;
      padding-right:10%;

  }
  .title{
      height:10%;
      text-align:center;
      /* padding:3%; */
      padding: 40px 0 20px 0;
      color:white;
  }
  
  .text{
      color:white;
      margin-bottom:2%;
      text-align:center;
      width:fit-content;
      font-family:-apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif;
      font-weight: 300;
  }
  .button{
      font-weight:bold;
  }
  .text2{
      color:white;
      margin-bottom:2%;
      text-align:center;
      width:100%;
      font-family:-apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif;
  }
  .buttonText{
      display:flex;
      flex-direction:column;
      justify-content:space-evenly;
      width:fit-content;
      height:auto;
      min-height:150px
  }

  .underline{
      margin-bottom: 40px;
  }
  @media screen and (max-width: 1000px) 
  {
      .maincontent
      {
          display:flex;
          flex-direction:column;
          padding: 5% 0% 5% 0%;
      }
      .buttonsection{
        display:flex;
        align-items:center;
        width:100%;
        padding-left:0;
      }
      .inputsection{
          width:100%;
          margin-bottom:10%;
          
      }
      .submitbtn{
        margin-left:0;
        justify-content:center;
      }
      .inputform{
          padding-left:10%
      }
      .row{
          flex-direction:column;
          label{
                width:100%;
                justify-content:center;
          }
      }
      .rowDescription{
        flex-direction:column;
        label{
            justify-content:center;
            width:100%;
            padding-right:0%;
            padding:2%;
        }
        
      }
      .inputform{
            border-right: 0px;
        }
      .textarea{
          width:100%
      }
      .textinput{
          width:100%;
      }
  }
  
`;

class Contact extends React.Component {
    constructor() {
        super();
    }

    render() {
        return (
            <StyledContainer>
                <div className="header divcenter">
                    <H3 className='title' color="#fff" center uppercase>Contact Us</H3>
                </div>

                <Underline
                    backgroundColor="#fff"
                    width="50px"
                    height="2px"
                    className="underline"
                />


                <div className="maincontent">


                    <div className="inputsection">

                        <form className="inputform">
                            <div className="row">
                                <label>EMAIL</label>
                                <INPUT className="textinput" padding type="email" name="" border margin borderRadius ></INPUT>
                            </div>
                            <div className="row">
                                <label>SUBJECT</label>
                                <INPUT className="textinput" padding type='text' name="" border margin borderRadius></INPUT>
                            </div>
                            <div className="rowDescription">
                                <label>DESCRIPTION</label>
                                <textarea className="textarea" rows="10" resize="none" ></textarea >
                            </div>
                            <div className="submitbtn">
                                <INPUT className="button"  margin borderRadius textAlign type="submit" value="SUBMIT" hoverBackgroundColor="white" hoverColor="#01C476"></INPUT>
                            </div>
                        </form>





                    </div>


                    <div className="buttonsection">
                        <div className="text">
                            <span>To estimate your project and watch more of our work</span>
                        </div>
                        <div className="buttonText">

                            <INPUT className="button" margin borderRadius textAlign type="submit" value="GET OUR PRICE" hoverBackgroundColor="white" hoverColor="#01C476"></INPUT>
                            <div className="text2">
                                <span>-or- </span>
                            </div>
                            <INPUT className="button" margin borderRadius textAlign type="submit" value="SEE OUR PORTFOLIO" hoverBackgroundColor="white" hoverColor="#01C476"></INPUT>


                        </div>


                    </div>


                </div>
            </StyledContainer>
        );
    }
}

export default Contact;