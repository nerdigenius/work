import React from "react";
import styled from "styled-components";
import { screenSize } from "../../constants/screenBreakpoints";
import { colors } from "../../constants/colors";
import H1 from "../ui/H1";
import H2 from "../ui/H2";
import H3 from "../ui/H3";
import Underline from "../ui/Underline";
import RightUnderline from "../ui/RightUnderline";
import Button from "../ui/Button";
import Shadowbtn from "../ui/largeShadowbtn";
const StyledContainer = styled.div`

  .container {
    ${"" /* margin-bottom: 80px; */}
    overflow-x: hidden;
  }

  .wrapper {
    padding: 10% 18%;
  }

.details{
  margin-bottom: 6%;
}

.hr-line{
  /* Style for "What is yo" */

color: #828282;

}
  .header-section {
    text-align: center;
    margin-bottom: 80px;
  }

  .title-section {
    margin-bottom: 20px;
  }

  .title h1 {
    display: inline;
  }

  .desc {
    margin-bottom: 40px;
    font-family: "Poppins";
    color: #00000070;
    display: inline-block;
  }

  .buttonlayout {
    display: flex;
    flex-wrap: wrap;
    justify-content: space-around;
    width: 100%;
    margin: 10% 0;
  }

  .imagesize {
    width: auto;
    height: 42px;
  }

  .form-body-text {
    display: flex;
    padding: 2% 6%;
  }

  .form {
    padding: 2% 2%;
    flex-basis: 84%;
  }

  body {
     ${"" /* font-family: Arial, Helvetica, sans-serif; */}
   
  }
  * {
    box-sizing: border-box;
  }

  ${
    "" /* input[type="text"],
  textarea {
    width: 100%;
    padding: 12px;
    border: none;
    border-radius: 4px;
    box-sizing: border-box;
    margin-top: 6px;
    margin-bottom: 16px;
    resize: vertical;
  } */
  }

  ${
    "" /* input[type="submit"] {
    background-color: #4caf50;
    color: white;
    padding: 12px 20px;
    border: none;
    border-radius: 4px;
    cursor: pointer;
  } */
  }

  ${"" /* input[type="submit"]:hover {
    background-color: #45a049;
  } */}



  input[type="text"],
  textarea {
    border-top: none;
    border-left: none;
    border-right: none;
    border-bottom: 1px solid #cdcdcd;
    outline: none;
    width: 90%;
    padding: 12px;
    box-sizing: border-box;
    margin-top: 6px;
    margin-bottom: 16px;
    resize: vertical;
    font-family: "Roboto", sans-serif !important;
    
  }

  .textarea{
  
    
    color: black;
    font-family: "Roboto", sans-serif;
    font-size: 16px;
    font-weight: 400;
    letter-spacing: 0.8px;
    line-height: 21px;
    height: 250px;
     }
   

  [placeholder]:focus::-webkit-input-placeholder {
    transition: text-indent 0.4s 0.4s ease;
    text-indent: -100%;
    opacity: 1;
  }

 

  .left-text-form {
    margin-top: 3%;
    color: #828282;
    font-family: Roboto;
    font-size: 14px;
    font-weight: 400;
    line-height: 21px;

    margin-right: 12%;
    text-align: left;
    flex-basis: 15%;
  }

  .left-text-form p {
    padding-bottom: 5%;
  }

  .left-text-form .underline {
    
  }
  .textbox-area {
    padding: 1% 15% 8% 15%;
  }

  .textbox-area p{
    color: #cdcdcd;
    font-family:   font-family: "Roboto", sans-serif;
    font-size: 12px;
    font-weight: 400;
    line-height: 21px;
}

.textbox::placeholder{
  font-size: 16px;
}


  .vl {
    border-left: 2px solid ${colors.primaryGreen};
    margin: 3% 0 40% 0;
    opacity: 0.3;
    flex-basis: 1%;
  }

  .form .button-est{
    margin-left: 15%;
  }

.mediamargin{
  ${"" /* width: 24%; */}
  height: auto;
  flex-basis: 33%;

}
  .mediamargin span {
    color: #01c476;
    font-family: "Roboto", sans-serif;
    font-size: 16px;
    font-weight: 400;
    height: 15%;
   
  }

.button-padding{
  flex-basis: 31.33%;
}
  
@media ${screenSize.size960} {

  .wrapper{
    padding: 30% 1%;
  }
}
@media ${screenSize.size650} {
    
  .container {
    margin: 60% 1% 30% 1%;
}

    .form-body-text {
    flex-direction: column;
  }

    .wrapper{
      padding: 1% 1%;
    }
    .form-body-text {
      padding: 1% 1%;
  }

   .textbox-area {
    padding: 1% 1% 14% 1%;
  }

  textarea {
   height: 200px;
}
    .vl{
      display: none;
    }

    .left-text-form p {
      text-align: center;
    }

    .underline{
      text-align: left;
    }

    input[type="text"], .textarea {
    
    width: 100%;

    }

   .form .button-est {
    text-align: center;
    margin-left: 0;
    width: auto;

    }

    .btn-resp-div{
      text-align: center;
    }



.underline-resp{
width: 250px;
}


}
`;

class Pricing extends React.Component {
  constructor() {
    super();
  }

  render() {
    return (
      <StyledContainer>
        <div className="container">
          <div className="wrapper">
            <div className="header-section">
              <div className="title-section">
                <div className="title">
                  {/* <div className="bar left"></div> */}
                  <H1>Check our exclusive Pricing</H1>
                  {/* <div className="bar right"></div> */}
                </div>
              </div>
              <div className="details">
                <div className="desc">
                  <p>
                    Get your desired project in flexible pricing on projects{" "}
                  </p>
                </div>
                <Underline
                  width="100px"
                  height="3px"
                  backgroundColor="#01c476"
                />
              </div>

              <div>
                <div className="desc">
                  <H2>What is your project about </H2>
                </div>
                <Underline className="underline-resp"
                  width="400px"
                  height="1px"
                  backgroundColor="#82828275"
                />
              </div>
            </div>

            <div className="buttonlayout">
              <div className="button-padding">
                <Shadowbtn
                  //height="15%"
                  //width="24%"
                  Border="1px solid white"
                  className="mediamargin"
                >
                  <img
                    className="imagesize"
                    src={require("../../static/Icon/webdev.png")}
                  />
                  <span>Web Development</span>
                </Shadowbtn>
              </div>
              <div className="button-padding">
                <Shadowbtn
                  //height="15%"
                  //width="24%"
                  Border="1px solid white"
                  className="mediamargin"
                >
                  <img
                    className="imagesize"
                    src={require("../../static/Icon/webdev.png")}
                  />
                  <span>Mobile Development</span>
                </Shadowbtn>
              </div>
              <div className="button-padding">
                <Shadowbtn
                  //height="15%"
                  //width="24%"
                  Border="1px solid white"
                  className="mediamargin"
                >
                  <img
                    className="imagesize"
                    src={require("../../static/Icon/webdev.png")}
                  />
                  <span>Digital Marketing</span>
                </Shadowbtn>
              </div>
            </div>

            <div className="form-body-text">
              <div className="left-text-form">
                <p>Fill the form and get an estimate</p>

                <Underline
                  className="underline"
                  width="100px"
                  height="3px"
                  backgroundColor="#01c476"
                />
              </div>
              <div className="vl" />
              <div className="form">
                <div className="textbox-area">
                  <form action="">
                    <input
                      className="textbox"
                      type="text"
                      //id="fname"
                      //name="firstname"
                      placeholder="Email"
                    />

                    <input
                      className="textbox"
                      type="text"
                      //id="lname"
                      //name="lastname"
                      placeholder="First Name"
                    />

                    <input
                      className="textbox"
                      type="text"
                      //id="lname"
                      //name="lastname"
                      placeholder="Last Name"
                    />
                    <input
                      className="textbox"
                      type="text"
                      //id="lname"
                      //name="lastname"
                      placeholder="Phone Number"
                    />

                    <textarea
                      className="textarea"
                      //id="subject"
                      //name="subject"
                      placeholder="Short description of your idea"
                    />

                    <p>
                      You can unsubscribe from these communications at any time.
                      For more information on how to unsubscribe, our privacy
                      practices please view our Privacy Policy.
                    </p>
                  </form>
                </div>

                {/* <input type="submit" value="GET ESTIMATION" /> */}
                
                
                <div className="btn-resp-div">
                <Button
                  className="button-est"
                  hoverBackgroundColor="#01c476"
                  hoverBorderColor="#01c476"
                  color="#01c476"
                  borderColor="#01c476"
                  hoverColor="#fff"
                  borderRadius="5px"
                >
                  GET ESTIMATION
                </Button>
                </div>
               
              </div>
            </div>
          </div>
        </div>
      </StyledContainer>
    );
  }
}

export default Pricing;
