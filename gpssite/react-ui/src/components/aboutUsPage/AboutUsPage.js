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
import tanvir from "../../static/Icon/tanvir.png";
import hafiz from "../../static/Icon/hafiz.png";
import ashiq from "../../static/Icon/ashiq.png";
import rashed from "../../static/Icon/rashed.png";
import nebir from "../../static/Icon/nebir.png";
import nabid from "../../static/Icon/nabid.png";

const StyledContainer = styled.div`
  .container {
    /* margin-bottom: 80px; */
  }

  .wrapper{
    padding: 15% 18% 10% 18%;
  }

  .header-section{
      text-align: center;
      margin-bottom: 80px;
  }

  .title-section{
      margin-bottom: 20px;
  }

  .title h1{
      display: inline;
      position: relative;
  }

  .title h1:after, .title h1:before{
        position: absolute;
        content: "";
        top: 50%;
        transform: translate(0, -50%);
        height: 50px;
        width: 120px;
        background-color: #01c476;
  }

  .title h1:after{
      left: 100%;
      margin-left: 20px;
  }

  .title h1:before{
      right: 100%;
      margin-right: 20px;
  }

  .desc{
      margin-bottom: 40px;
      font-family: 'Poppins';
      color: #00000070;
  }

  .footer-section{
      text-align: center;
      margin-top: 60px;
  }

  .teams{
      display: flex;
      justify-content: space-between;
  }

  .team{
    flex-basis: 31.33%;
    text-align: center;
    margin-bottom: 40px;
    box-sizing: border-box;
  }

  .card{
    /* display: inline-block; */
    box-shadow: 0px 4px 4px -1px rgba(0,0,0,0.1);
  }

  .image-holder img{
      max-width: 100%;
      width: 100%;
  }

  .image-detail{
      /* margin-top: 20px; */
      padding: 20px 0;
      font-family: 'Poppins';
  }

  .member-title{
    font-size: 1.7em;
    font-weight: 600;
  }

  .member-role{
      font-size: 0.8em;
      color: #00000070;
  }

  .button-section{
      margin-top: 40px;
      display: flex;
      justify-content: center;
  }

  .button-section div{
      margin-right: 20px;
  }

  .button-section div:last-child{
      margin-right: 0;
  }

  @media ${screenSize.size960} {
      .wrapper{
          padding: 30% 5% 15% 5%;
      }
      .title h1:after, .title h1:before{
          content: none;
      }
  }

  @media ${screenSize.size650}{
      .wrapper{
          padding: 60% 10% 30% 10%;
      }
      .teams{
          flex-direction: column;
      }

      .header-section{
          margin-bottom: 60px;
      }

      .member-title{
          font-size: 1.5em;
      }

      .button-section{
        flex-direction: column;
        display: inline-block;
      }

      .button-section div{
        width: 75%;
        margin-bottom: 10px;
        margin-right: 0;
      }
  }
`;

class Projects extends React.Component {
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
                                    <H1>Our Executive Team Member</H1>
                                    {/* <div className="bar right"></div> */}
                                </div>
                            </div>
                            <div className="details">
                                <div className="desc">
                                    <p>Meet our crazy friendly awesome team member. We always create cool stuff for you</p>
                                </div>
                                <Underline width="100px" height="3px" backgroundColor="#01c476"></Underline>
                            </div>
                        </div>

                        <div className="team-section">
                            <div className="teams">
                                <div className="team">
                                    <div className="card">
                                        <div className="image-holder">
                                            <img src={tanvir}></img>
                                        </div>
                                        <div className="image-detail">
                                            <div className="member-title">Tan Master</div>
                                            <div className="member-role">Managing Director</div>
                                        </div>
                                    </div>
                                </div>
                                <div className="team">
                                    <div className="card">
                                        <div className="image-holder">
                                            <img src={hafiz}></img>
                                        </div>
                                        <div className="image-detail">
                                            <div className="member-title">Mr. Hafeez</div>
                                            <div className="member-role">Business Development Manager</div>
                                        </div>
                                    </div>
                                </div>
                                <div className="team">
                                    <div className="card">
                                        <div className="image-holder">
                                            <img src={ashiq}></img>
                                        </div>
                                        <div className="image-detail">
                                            <div className="member-title">Rocking Ashiq</div>
                                            <div className="member-role">Software Engineer</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div className="teams">
                                <div className="team">
                                    <div className="card">
                                        <div className="image-holder">
                                            <img src={rashed}></img>
                                        </div>
                                        <div className="image-detail">
                                            <div className="member-title">Metalic Rashed</div>
                                            <div className="member-role">Software Engineer</div>
                                        </div>
                                    </div>
                                </div>
                                <div className="team">
                                    <div className="card">
                                        <div className="image-holder">
                                            <img src={nabid}></img>
                                        </div>
                                        <div className="image-detail">
                                            <div className="member-title">xD Nabid</div>
                                            <div className="member-role">Software Engineer</div>
                                        </div>
                                    </div>
                                </div>
                                <div className="team">
                                    <div className="card">
                                        <div className="image-holder">
                                            <img src={nebir}></img>
                                        </div>
                                        <div className="image-detail">
                                            <div className="member-title">Gradient Nebir</div>
                                            <div className="member-role">UI/UX Designer</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div className="footer-section">
                            <div className="desc">
                                <p>To check out our crazy stuff and awesome work</p>
                            </div>
                            <Underline width="100px" height="2px" backgroundColor="#01c476"></Underline>
                            <div className="button-section">
                                <Button
                                    hoverBackgroundColor="#01c476"
                                    hoverBorderColor="#01c476"
                                    color="#01c476"
                                    borderColor="#01c476"
                                    hoverColor="#fff">
                                    SEE OUR PORTFOLIO
                                </Button>
                                <Button
                                    hoverBackgroundColor="#01c476"
                                    hoverBorderColor="#01c476"
                                    color="#01c476"
                                    borderColor="#01c476"
                                    hoverColor="#fff">
                                    SEE OUR BLOGS
                                </Button>
                            </div>
                        </div>
                    </div>
                </div>
            </StyledContainer>
        );
    }
}

export default Projects;
