import React from "react";
import styled from "styled-components";
import { screenSize } from "../../constants/screenBreakpoints";
import H3 from "../ui/H3";
import { colors } from "../../constants/colors";
import Underline from "../ui/Underline";
import clientPic from "../../static/Icon/Buyer_image.png";
// import ReactDOM from "react-dom";
// import "react-responsive-carousel/lib/styles/carousel.min.css";
// import { Carousel } from "react-responsive-carousel";

import AliceCarousel from "react-alice-carousel";
import "react-alice-carousel/lib/alice-carousel.css";
import leftIcon from "../../static/Icon/Right_symbol.svg";
import rightIcon from "../../static/Icon/Left_symbol.svg";

const StyledContainer = styled.div`
  @import "react-alice-carousel/lib/alice-carousel.css";
  .carousel .slide {
    background-color: white;
  }

  .slider-container {
    position: relative;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
  }

  .slideSection {
    padding: 0 9%;
  }

  .prev-container{
    float:left;
  }

  .next-container{
    float:right;
  }

  .button-container{
    position: absolute;
    top: 50%;
    transform: translate(0, -50%);
    width: 100%;
  }

  .carousel .slide img {
    width: auto;
  }

  ${"" /* .carousel.carousel-slider .control-arrow{
    background: rgba(0,0,0,1);
  } */}

  .wrapper {
    padding: 3% 9%;
  }

  .headerSection {
    margin-bottom: 30px;
  }
  .cardImg {
    margin-bottom: 15px;
  }

  .card {
    background-color: #fafafa;
    padding: 2% 7% 8% 7%;
    text-align: center;
  }

  .paraDesc {
    color: #373737;
    font-family: Roboto;
    letter-spacing: 0.7px;
    line-height: 24px;
  }

  .title {
    margin-bottom: 20px;
  }

  .underline {
    margin-bottom: 20px;
  }

  .clientName {
    margin-bottom: 3px;
    font-family: Roboto;
    font-size: 0.9em;
    font-weight: 700;
    letter-spacing: 0.6px;
  }
  .role {
    font-family: Roboto;
    font-size: 0.7em;
    letter-spacing: 0.5px;
  }

  .cardHeader {
    margin-bottom: 40px;
  }

  @media ${screenSize.size960} {
    .wrapper {
      padding: 12% 7%;
    }
  }
`;

class Client extends React.Component {
  constructor() {
    super();
  }

  items = [1, 2, 3, 4, 5];

  state = {
    currentIndex: 0,
    responsive: { 1024: { items: 3 } },
    galleryItems: this.galleryItems()
  };

  slideTo = i => this.setState({ currentIndex: i });

  onSlideChanged = e => this.setState({ currentIndex: e.item });

  slideNext = () =>
    this.setState({ currentIndex: this.state.currentIndex + 1 });

  slidePrev = () =>
    this.setState({ currentIndex: this.state.currentIndex - 1 });

  thumbItem = (item, i) => <span onClick={() => this.slideTo(i)}> </span>;

  galleryItems() {
    return this.items.map(i => <h2 key={i}> {i}</h2>);
  }

  render() {
    return (
      <StyledContainer>
        <div className="container">
          <div className="wrapper">
            <div className="headerSection">
              <H3
                className="title"
                uppercase
                center
                color={colors.primaryGreen}
              >
                client thoughts
              </H3>

              <Underline
                backgroundColor={colors.primaryGreen}
                width="50px"
                height="2px"
                className="underline"
              />
            </div>

            <div className="slider-container">
              <div className="button-container">
                <div className="prev-container">
                  <div onClick={() => this.Carousel._slidePrev()}>
                  <img src={leftIcon} />
                  </div>
                </div>
                <div className="next-container">
                <div onClick={() => this.Carousel._slideNext()}>
                  <img src={rightIcon} />
                  </div>
                </div>
              </div>
              <div className="slideSection">
                {/* infiniteLoop={true}  autoPlay={true} */}

                <AliceCarousel
                  mouseDragEnabled
                  dotsDisabled={true}
                  buttonsDisabled={true}
                  items={this.state.galleryItems}
                  ref={el => (this.Carousel = el)}
                  // infiniteLoop={true}
                  // autoPlay={true}
                >
                  <div className="card">
                    <div className="cardHeader">
                      <div className="cardImg">
                        <img src={clientPic} />
                      </div>
                      <div className="cardDetail">
                        <div className="clientName">Mike Cartson</div>

                        <div className="role">CEO, InterTech</div>
                      </div>
                    </div>
                    <div className="cardDsc">
                      <p className="paraDesc">
                        Lorem Ipsum is simply dummy text of the printing and
                        typesetting industry. Lorem Ipsum has been the
                        industry's standard dummy text ever since the 1500s,
                        when an unknown printer took a galley of type and
                        scrambled it to make a type specimen book. It has
                        survived not only five centuries, but also the leap into
                        electronic typesetting, remaining essentially unchanged.
                        electronic typesetting, remaining essentially unchanged.
                      </p>
                    </div>
                  </div>

                  <div className="card">
                    <div className="cardHeader">
                      <div className="cardImg">
                        <img src={clientPic} />
                      </div>
                      <div className="cardDetail">
                        <div className="clientName">Mike Cartson</div>

                        <div className="role">CEO, InterTech</div>
                      </div>
                    </div>
                    <div className="cardDsc">
                      <p className="paraDesc">
                        Lorem Ipsum is simply dummy text of the printing and
                        typesetting industry. Lorem Ipsum has been the
                        industry's standard dummy text ever since the 1500s,
                        when an unknown printer took a galley of type and
                        scrambled it to make a type specimen book. It has
                        survived not only five centuries, but also the leap into
                        electronic typesetting, remaining essentially unchanged.
                        electronic typesetting, remaining essentially unchanged.
                      </p>
                    </div>
                  </div>
                </AliceCarousel>

                <nav>{this.items.map(this.thumbItem)}</nav>
                {/* <button onClick={() => this.Carousel._slidePrev()}>
                Prev button
              </button> */}
              </div>
            </div>
          </div>
        </div>
      </StyledContainer>
    );
  }
}

export default Client;
