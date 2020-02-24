using NUnit.Framework;
using Scene2d.CommandBuilders;
using Scene2d.Exceptions;

namespace Tests
{
    delegate void addStrings(string str);

    [TestFixture]
    public class TestsCommandBuilders
    {
        #region CheckCommandProducer
        [Test]
        public void AppendLine_One_String_Command_Check_IsCommandReady()
        {
            //примеры однострочных комманд
            string[] arrayForRegex =
            {
                "add circle BASE (0,0) radius 600",
                "add rectangle R3 (-424, -212) (424, 212)",
                "move R2 (600, 0)",
                "delete BASE",
                "rotate R1 45",
                "reflect vertically T4",
                "copy G1 to G2",
                "print circumscribing rectangle for BASE",
                "group R1, R2 as G1"
            };
            var testCommandProducer = new CommandProducer();

            foreach(var el in arrayForRegex)
            {
                testCommandProducer.AppendLine(el);
                Assert.That(testCommandProducer.IsCommandReady, Is.True);
                testCommandProducer.ToNull();
            }

        }

        [Test]
        public void AppendLine_Multi_String_Command_Check_IsCommandReady()
        {
            //примеры многострочных комманд
            string[] arrayForRegex =
            {
                "add polygon T1",
                "add point (0, 0)",
                "add point (-300, -300)",
                "add point (-300, 300)"
            };
            var testCommandProducer = new CommandProducer();

            foreach (var el in arrayForRegex)
            {
                testCommandProducer.AppendLine(el);
                Assert.That(testCommandProducer.IsCommandReady, Is.False);
            }
            testCommandProducer.AppendLine("end polygon");
            Assert.That(testCommandProducer.IsCommandReady, Is.True);
        }

        [Test]
        public void AppendLine_Add_Comments_Multi_String_Command_Check_IsCommandReady()
        {
            //работа с комментрариями
            string[] arrayForRegex =
            {
                "add polygon T1 #comment",
                "add point (0, 0) # comment",
                "add point (-300, -300) #comment",
                "# comment",
                "#comment",
                "add point (-300, 300) # comment",
                "#comment"
            };
            var testCommandProducer = new CommandProducer();

            foreach (var el in arrayForRegex)
            {
                testCommandProducer.AppendLine(el);
                Assert.That(testCommandProducer.IsCommandReady, Is.False);
            }
            testCommandProducer.AppendLine("end polygon # comment");
            Assert.That(testCommandProducer.IsCommandReady, Is.True);
        }

        [Test]
        public void AppendLine_Add_Comments_One_String_Command_Check_IsCommandReady()
        {
            //работа с комментрариями
            string[] arrayForRegex =
            {
                "add circle BASE (0,0) radius 600 #comment",
                "add rectangle R3 (-424, -212) (424, 212) # comment"
            };
            var testCommandProducer = new CommandProducer();

            foreach (var el in arrayForRegex)
            {
                testCommandProducer.AppendLine(el);
                Assert.That(testCommandProducer.IsCommandReady, Is.True);
                testCommandProducer.ToNull();
            }
        }

        [Test]
        public void AppendLine_Bad_Format_Command_In_Multi_String_Command_Check_IsCommandReady()
        {
            //работа с комментрариями
            string[] arrayGoodStr =
            {
                "add point (0, 0)",
                "add point (-300, -300)",
                "add point (-300, 300)"
            };

            //значения могут быть различными
            string[] arrayBadStr =
            {
                "add circle BASE (0,0) radius 600",
                "add circle BASE (0,0) radius 600",
                "add circle BASE (0,0) radius 600",
                "add circle BASE (0,0) radius 600"
            };

            var testCommandProducer = new CommandProducer();
            addStrings addStr = testCommandProducer.AppendLine;
            

            foreach (var erStr in arrayBadStr)
            {
                addStr("add polygon T1");
                foreach (var el in arrayGoodStr)
                {
                    addStr(el);
                    Assert.That(testCommandProducer.IsCommandReady, Is.False);
                    Assert.Throws<UnexpectedEndOfPolygonExeption>(() => addStr(erStr), erStr);
                    //тоже работает
                    //Assert.Throws<UnexpectedEndOfPolygonExeption>(()
                    //=> testCommandProducer.AppendLine(el));
                }
                addStr("end polygon");
                Assert.That(testCommandProducer.IsCommandReady, Is.True);
                testCommandProducer.ToNull();
            }

        }
        #endregion

        #region CheckFiguresCommands

        #region AddCircle
        [Test]
        public void AddСircleCommandBuilder_Check_BadCircleRadiusException()
        {
            // проверка на равенство 0. Отрицательные значения 
            // отсеиваются с помощью Regex
            string[] arrayOfRadiuses = { "0.00000000001", "0" };
            string halfCommand = "add circle BASE (0,0) radius ";

            addStrings addStr= new AddСircleCommandBuilder().AppendLine;
            
            foreach(var el in arrayOfRadiuses)
            {
                halfCommand += el;
                Assert.Throws<BadCircleRadiusException>(() => addStr(halfCommand));
            }
        }

        [Test]
        public void AddСircleCommandBuilder_Check_BadFormatException()
        {
            string[] arrayOfCommands =
            {
                "add circle BASE (0,0) radius",
                "add circle BASE (0,0) 23",
                "add circle BASE radius 23",
                "add circle (0,0) radius 23",
                "add circle BASE (0.0) radius 23",
                "add circle BASE(0,0)radius23"
            };

            addStrings addStr = new AddСircleCommandBuilder().AppendLine;

            foreach (var el in arrayOfCommands)
            {
                Assert.Throws<BadFormatException>(() => addStr(el));
            }
        }
        #endregion

        #region AddRectangle
        [Test]
        public void AddRectangleCommandBuilder_Check_BadFormatException()
        {
            string[] arrayOfCommands =
            {
                "add rectangle R1 (-212, -212)",
                "add rectangle R1",
                "add rectangleR1 (-212, -212) (212, 212)",
                "add rectangle R1(-212, -212) (212, 212)",
                "add rectangle R1 (-212f, -212) (212, 212)",
                "add rectangle R1 (-212. -212) (212. 212)",
            };

            addStrings addStr = new AddRectangleCommandBuilder().AppendLine;

            foreach (var el in arrayOfCommands)
            {
                Assert.Throws<BadFormatException>(() => addStr(el));
            }
        }
        #endregion

        #region AddPolygon

        [Test]
        public void AddPolygonCommandBuilder_Check_UnexpectedEndOfPolygonExeption_When_Add_Point()
        {
            addStrings addStr = new CommandProducer().AppendLine;
            addStr("add polygon T1");

            string[] arrayOfCommands =
            {
                "addpoint (0, 0)",
                "add point(0, 0)",
                "add rectangle R1 (-212, -212)",
                "add circle BASE (0,0) 23"
            };

            foreach (var el in arrayOfCommands)
            {
                Assert.Throws<UnexpectedEndOfPolygonExeption>(() => addStr(el));
            }
        }

        [Test]
        public void AddPolygonCommandBuilder_BadFormatException_When_Add_Point()
        {
            addStrings addStr = new CommandProducer().AppendLine;
            addStr("add polygon T1");

            string[] arrayOfCommands =
            {
                "add point (00000141.1.1, 0)",
                "add point (0. 0)",
                "add point (78f, 0)",
                "add point 0, 0"
            };

            foreach (var el in arrayOfCommands)
            {
                Assert.Throws<BadFormatException>(() => addStr(el));
            }
        }

        [Test]
        public void AddPolygonCommandBuilder_BadPolygonPointNumberException_Check()
        {
            addStrings addStr = new CommandProducer().AppendLine;

            string[] arrayOfCommands =
            {
                "add polygon T1",
                "add point (0, 0)",
                "add point (-300, -300)",
            };

            foreach (var el in arrayOfCommands)
            {
                addStr(el);
            }

            Assert.Throws<BadPolygonPointNumberException>(() => addStr("end polygon"));
        }

        [Test]
        public void AddPolygonCommandBuilder_BadPolygonPointException_Check()
        {
            addStrings addStr = new CommandProducer().AppendLine;

            string[] arrayOfCommands =
            {
                "add polygon T1",
                "add point (0, 0)"
            };

            foreach (var el in arrayOfCommands)
            {
                addStr(el);
            }

            Assert.Throws<BadPolygonPointException>(() => addStr("add point (0, 0)"));
        }
        #endregion

        #region Move
        [Test]
        public void MoveFigureCommandBuilder_BadFormatException_Check()
        {
            addStrings addStr = new MoveFigureCommandBuilder().AppendLine;

            string[] arrayOfCommands =
            {
                "moveT3 (150, -150)",
                "move T3(150, -150)",
                "move T3 (150-150)",
                "move T3 150, -150)",
                "moveT3 (150, -150)",
                "movescene (150, -150)",
                "move scene(150, -150)"
            };

            foreach (var el in arrayOfCommands)
            {
                Assert.Throws<BadFormatException>(() => addStr(el));
            }

            
        }
        #endregion

        #region Delete
        [Test]
        public void DeleteFigureCommandBuilder_BadFormatException_Check()
        {
            addStrings addStr = new DeleteFigureCommandBuilder().AppendLine;

            string[] arrayOfCommands =
            {
                "deleteT3",
                "deletescene"
            };

            foreach (var el in arrayOfCommands)
            {
                Assert.Throws<BadFormatException>(() => addStr(el));
            }


        }
        #endregion

        #region Rotate
        [Test]
        public void RotateComandBuilder_BadFormatException_Check()
        {
            addStrings addStr = new RotateComandBuilder().AppendLine;

            string[] arrayOfCommands =
            {
                "rotateR1 45",
                "rotate R145",
                "rotate R1 er",
                "rotate R*1 er",
                "rotate s cene"
            };

            foreach (var el in arrayOfCommands)
            {
                Assert.Throws<BadFormatException>(() => addStr(el));
            }


        }
        #endregion

        #region Reflect
        [Test]
        public void ReflectCommandBuilder_BadFormatException_Check()
        {
            addStrings addStr = new ReflectCommandBuilder().AppendLine;

            string[] arrayOfCommands =
            {
                "reflectvertically R4",
                "reflect verticallyR4",
                "reflect vertically",
                "reflect horizontaly e",
                "horizontally"
            };

            foreach (var el in arrayOfCommands)
            {
                Assert.Throws<BadFormatException>(() => addStr(el));
            }

        }
        #endregion

        #region Copy
        [Test]
        public void CopyCommandBuilder_BadFormatException_Check()
        {
            addStrings addStr = new CopyCommandBuilder().AppendLine;

            string[] arrayOfCommands =
            {
                "copyC1 to C4",
                "copy C1to C4",
                "copy C1 toC4",
                "copy C1 to ",
                "copy C 1 to C4"
            };

            foreach (var el in arrayOfCommands)
            {
                Assert.Throws<BadFormatException>(() => addStr(el));
            }

        }
        #endregion

        #region PrintCircumscribingRectangle
        [Test]
        public void PrintCircumscribingRectangleCommandBuilder_BadFormatException_Check()
        {
            addStrings addStr = new CopyCommandBuilder().AppendLine;

            Assert.Throws<BadFormatException>(() => addStr("print circumscribing rectangle for scene"));

        }
        #endregion
        #endregion
    }
}