using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    class DetectForceConvergence
    {
        //If the sum of all velocities is below this number,
        //the system is considered to have converged
       
        public readonly LayoutStep step;
        public DetectForceConvergence(PedigreeModel model, PedigreeController layoutEngine)
        {
            step = delegate()
            {
                if (model.individuals.Count < 2)
                {
                    layoutEngine.SetMode(PedigreeController.MANUAL);
                    return;
                }

                if (model.io.mouseDown == false)
                {
                    double delta = 0;
                    //bool moving = false;
                    foreach (PedigreeIndividual pi in model.individuals)
                    {
                        double meanX = 0;
                        double meanY = 0;
                        if (model.PositionHistoryX.ContainsKey(pi.HraPerson.relativeID))
                        {
                            model.PositionHistoryX[pi.HraPerson.relativeID].Add(pi.point.x);
                            if (model.PositionHistoryX[pi.HraPerson.relativeID].Count > model.parameters.PositionHistoryDepth)
                                model.PositionHistoryX[pi.HraPerson.relativeID].RemoveAt(0);
                        }
                        else
                        {
                            List<double> past = new List<double>();
                            past.Add(pi.point.x);
                            model.PositionHistoryX.Add(pi.HraPerson.relativeID, past);
                        }
                        foreach (double cur in model.PositionHistoryX[pi.HraPerson.relativeID])
                        {
                            meanX += cur;
                        }
                        meanX = meanX / ((double)(model.PositionHistoryX[pi.HraPerson.relativeID].Count));


                        if (model.PositionHistoryY.ContainsKey(pi.HraPerson.relativeID))
                        {
                            model.PositionHistoryY[pi.HraPerson.relativeID].Add(pi.point.y);
                            if (model.PositionHistoryY[pi.HraPerson.relativeID].Count > model.parameters.PositionHistoryDepth)
                                model.PositionHistoryY[pi.HraPerson.relativeID].RemoveAt(0);
                        }
                        else
                        {
                            List<double> past = new List<double>();
                            past.Add(pi.point.y);
                            model.PositionHistoryY.Add(pi.HraPerson.relativeID, past);
                        }
                        foreach (double cur in model.PositionHistoryY[pi.HraPerson.relativeID])
                        {
                            meanY += cur;
                        }
                        meanY = meanY / ((double)(model.PositionHistoryY[pi.HraPerson.relativeID].Count));

                        if (model.TargetPositions.ContainsKey(pi.HraPerson.relativeID))
                        {
                            delta += Math.Abs((model.TargetPositions[pi.HraPerson.relativeID].x - meanX));
                            delta += Math.Abs((model.TargetPositions[pi.HraPerson.relativeID].y - meanY));
                            model.TargetPositions[pi.HraPerson.relativeID].x = meanX;
                            model.TargetPositions[pi.HraPerson.relativeID].y = meanY;
                        }
                        else
                        {
                            PointWithVelocity pos = new PointWithVelocity();
                            pos.x = meanX;
                            pos.y = meanY;
                            model.TargetPositions.Add(pi.HraPerson.relativeID, pos);
                            delta += Math.Abs(meanX);
                            delta += Math.Abs(meanY);
                        }

                    }

                    if (delta / model.individuals.Count < 0.05)
                    {
                        if (model.converged == false)
                        {
                            if (layoutEngine.ModelConvergedCallback != null)
                                layoutEngine.ModelConvergedCallback.Invoke();
                        }
                        model.converged = true;
                    }
                    else
                    {
                        model.converged = false;
                    }
                }
            };
        }

        private bool ForcesHaveConverged(PedigreeModel model)
        {
            double avg = 0;
            foreach (PointWithVelocity point in model.points)
                avg += Math.Abs(point.dx) + Math.Abs(point.dy);
            avg /= model.points.Count;
            return avg < model.parameters.avgVelocityThreshold;
        }
    }
}



/*
double deltaOne = 0;
double deltaTwo = 0;
double deltaThree = 0;
foreach (PedigreeIndividual pi in model.individuals)
{
    if (model.TargetPositions.ContainsKey(pi.relativeID))
    {
        deltaOne = (model.TargetPositions[pi.relativeID].x - pi.point.x);
        model.TargetPositions[pi.relativeID].x = pi.point.x;
    }
    else
    {
        PointWithVelocity pos = new PointWithVelocity();
        pos.x = pi.point.x;
        pos.y = pi.point.y;
        model.TargetPositions.Add(pi.relativeID, pos);
        deltaOne += pos.x;
    }

    if (model.PositionHistory.ContainsKey(pi.relativeID))
    {
        deltaTwo = (model.OscillationBufferPositions[pi.relativeID].x - model.TargetPositions[pi.relativeID].x);
        model.OscillationBufferPositions[pi.relativeID].x = model.TargetPositions[pi.relativeID].x;
    }
    else
    {
        PointWithVelocity pos = new PointWithVelocity();
        pos.x = model.TargetPositions[pi.relativeID].x;
        pos.y = model.TargetPositions[pi.relativeID].x;
        //model.OscillationBufferPositions.Add(pi.relativeID, pos);
        deltaTwo += pos.x;
    }
    deltaThree += Math.Abs(deltaOne + deltaTwo);
}
if (deltaOne < 1)
{
    //if (model.couplesGraphIsPlanar && model.layoutIsValid)
    layoutEngine.SetMode(PedigreeController.MANUAL);
}
//else if (model.couplesGraphIsPlanar && model.layoutIsValid && model.parameters.repelIndividualSetsStrength > 20)
//{
//    layoutEngine.SetMode(PedigreeController.MANUAL);
//}
//model.forcesHaveConverged = ForcesHaveConverged(model);

*/