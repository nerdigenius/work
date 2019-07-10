import React, { Component } from 'react';
import { createGlobalStyle } from 'styled-components';
import styled from "styled-components";

import Underline from "../ui/Underline";
import H3 from "../ui/H3";
import H1 from "../ui/H1";
import Grid1 from "./Grid1";
import Grid2 from "./Grid2";
import ButtonLayout from "./ButtonLayout";
import { Spring } from 'react-spring/renderprops';
import { colors } from '../../constants/colors';
import Button from "../ui/Button";







const StyledContainer = styled.div`
  height: fit-content;
  background-color: #FFFFFF;
  padding:0 15%;
  .buttonlayout{
      display:flex;
      justify-content:space-around;
      width:100%;
      margin:10% 0;
  }
  .title{
      display: flex;
      width:100%;
      height:fit-content;
      margin:1% 0;

  }
  .colorDiv{
      background-color: #01c476;
      height:auto;
      flex-grow:1;
      margin:2% 0;
      margin-left:2%
  }
  .imagesize{

      width: auto;
      height:42px;
  }
  .buttonHover:hover{
    color:white;
    background-color:${colors.primaryGreen};

  }
  .gridContainer
  {
    display: grid;
    height:fit-content;
  grid-template-columns: repeat(4, 1fr);
  grid-template-rows: repeat(5, 251px);
  grid-auto-flow: dense;
  grid-gap:5px;
  }
  .gridItem{
    grid-column-end: span 4;
  }
  .gridItem:nth-child(1) {
  grid-row: 1;
  grid-column-end: span 2;
  grid-row-end: span 2;
}
.gridItem:nth-child(2) {
  grid-row: 1;
  grid-column-end: span 2;
  grid-row-end: span 1;
}
.gridItem:nth-child(3) {
  grid-row: 2;
  grid-column-end: span 1;
  grid-row-end: span 1;
}
.gridItem:nth-child(4) {
  grid-row: 2;
  grid-column-end: span 1;
  grid-row-end: span 1;
}
.gridItem:nth-child(5) {
  grid-row: 3;
  grid-column-end: span 1;
  grid-row-end: span 1;
}
.gridItem:nth-child(6) {
  grid-row: 3;
  grid-column-end: span 3;
  grid-row-end: span 1;
}
.gridItem:nth-child(7) {
  grid-row: 4;
  grid-column-end: span 2;
  grid-row-end: span 1;
}
.gridItem:nth-child(8) {
  grid-row: 4;
  grid-column-end: span 1;
  grid-row-end: span 1;
}
.gridItem:nth-child(9) {
  grid-row: 4;
  grid-column-end: span 1;
  grid-row-end: span 1;
}

  .gridImage{
      display:block;
      width:100%;
      height: 100%;
      object-fit:cover ;
  }
  .imagesize{

      width: auto;
      height:42px;
  }
  .gridContainer
  {
    display: grid;
    height:fit-content;
  grid-template-columns: repeat(4, 1fr);
  grid-template-rows: repeat(5, 251px);
  grid-auto-flow: dense;
  grid-gap:5px;
  }
  .gridItem{
    grid-column-end: span 4;
  }
  .gridItem:nth-child(1) {
  grid-row: 1;
  grid-column-end: span 2;
  grid-row-end: span 2;
}
.gridItem:nth-child(2) {
  grid-row: 1;
  grid-column-end: span 2;
  grid-row-end: span 1;
}
.gridItem:nth-child(3) {
  grid-row: 2;
  grid-column-end: span 1;
  grid-row-end: span 1;
}
.gridItem:nth-child(4) {
  grid-row: 2;
  grid-column-end: span 1;
  grid-row-end: span 1;
}
.gridItem:nth-child(5) {
  grid-row: 3;
  grid-column-end: span 1;
  grid-row-end: span 1;
}
.gridItem:nth-child(6) {
  grid-row: 3;
  grid-column-end: span 3;
  grid-row-end: span 1;
}
.gridItem:nth-child(7) {
  grid-row: 4;
  grid-column-end: span 2;
  grid-row-end: span 1;
}
.gridItem:nth-child(8) {
  grid-row: 4;
  grid-column-end: span 1;
  grid-row-end: span 1;
}
.gridItem:nth-child(9) {
  grid-row: 4;
  grid-column-end: span 1;
  grid-row-end: span 1;
}

  .gridImage{
      display:block;
      width:100%;
      height: 100%;
      object-fit:cover ;
  }

  
  .text2{
      display:flex;
      justify-content:center;
      color:#01c476;
      margin:10px 0;
  }
  .titleDiv{
      margin: 10% 0;
  }
  .text{
      font-size:45px;
      font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif;
      font-weight: 700;
  }

  
  .text2{
      display:flex;
      justify-content:center;
      color:#01c476;
      margin:10px 0;
  }
  .titleDiv{
      margin: 10% 0;
  }
  .text{
      font-size:45px;
      font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif;
      font-weight: 700;
  }
  .visible{
      display:none;
  }
  .gridVisible{
      display:none
  }
  .gridContainer2{
    display: grid;
    height:fit-content;
  grid-template-columns: repeat(2, 1fr);
  grid-auto-rows: 1fr;
  grid-auto-flow: dense;
  grid-gap:20px;
  margin:3% 0;
  }
  
  @media screen and (max-width: 700px) {
      .buttonlayout{
        flex-wrap:wrap;
        align-items:center;
      }
      .mediamargin{
          margin:20px 0;
          min-width:120px;
      }
      .visible{
          display:flex;
          flex-direction:column;
      }
      .gridContainer{
          display:none;
      }
      .gridImage{
          margin:2% 0;
      }
      .gridContainer2{
    
  grid-template-columns: repeat(1, 1fr);
  
  }

  }

  

  
  

`;

class ProjectsGrid extends Component {
    constructor(props) {
        super(props)
        this.state = {
            buttonText: "VIEW MORE",
            visibility: false,
            columnVisible: "visible",
            gridContent: this.returnGridContainer()
        }
        this.onShowmoreclick = this.onShowmoreclick.bind(this);
        this.returnGridContainer = this.returnGridContainer.bind(this)

    }



    returnGridContainer() {
        return (
            <Spring
                from={{ opacity: 0 }}
                to={{ opacity: 1 }}>
                {props => (
                    <div style={props}>
                        <Grid1 />
                    </div>
                )}

            </Spring>

        )
    }

    returnGridContainer2() {

        return (
            <Spring
                from={{ opacity: 0 }}
                to={{ opacity: 1 }}>
                {props => (
                    <div style={props}>
                        <Grid2 />
                    </div>
                )}

            </Spring>
        )
    }

    onShowmoreclick() {

        if (this.state.visibility === false) {
            this.setState({
                visibility: true,
                gridvisible2: "gridContainer2",
                gridvisible: "gridVisible",
                buttonText: "SHOW LESS",
                columnVisible: "gridVisible",
                gridContent: this.returnGridContainer2()
            })
        }

        else {
            this.setState({
                visibility: false,
                gridvisible2: "gridVisible",
                gridvisible: "gridContainer",
                buttonText: "VIEW MORE",
                columnVisible: "visible",
                gridContent: this.returnGridContainer()
            })
        }
    }

    render() {

        return (
            <StyledContainer style={{ marginTop: "150px" }}>
                <div className="titleblock">
                    <div className="title">
                        <H1 >IT ALWAYS SEEMS IMPOSSIBLE</H1>
                        <div className="colorDiv"></div>
                    </div>
                    <div className="title">
                        <H1>UNTIL IT'S DONE</H1>
                        <div className="colorDiv"></div>
                    </div>
                </div>
                <ButtonLayout />



                

               

                {this.state.gridContent}




                <div className={this.state.columnVisible}>
                    <img className="gridImage" src={require('../../static/Icon/grid1.png')} />
                    <img className="gridImage" src={require('../../static/Icon/grid3.png')} />
                    <img className="gridImage" src={require('../../static/Icon/grid6.png')} />

                </div>

                

               
                <div className="titleDiv" >
                    <div className="divcenter text2" >
                    <Button onClick={this.onShowmoreclick} className="buttonHover">{this.state.buttonText}</Button>
                    </div>
                    
                </div>
            </StyledContainer>

        );
    }
}

export default ProjectsGrid;