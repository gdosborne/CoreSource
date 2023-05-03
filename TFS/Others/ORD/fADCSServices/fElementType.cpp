#include "fElementType.h"

#include <crtdbg.h>

// .Net namespaces
using namespace System;

// Custom namespaces
using namespace aCONF;

static fElementType::fElementType()
{
  pXlatTags->Add("address", fRT::TypeIDs::ADDRESS);
  pXlatTags->Add("alias", fRT::TypeIDs::ALIAS);
  pXlatTags->Add("alignment", fRT::TypeIDs::ALIGNMENT);
  pXlatTags->Add("angle", fRT::TypeIDs::ANGLE);
  pXlatTags->Add("annotationtypes", fRT::TypeIDs::ANNOTATIONTYPES);
  pXlatTags->Add("arcfillcolor", fRT::TypeIDs::ARCFILLCOLOR);
  pXlatTags->Add("autoscale", fRT::TypeIDs::AUTOSCALE);
  pXlatTags->Add("axis", fRT::TypeIDs::AXIS);
  pXlatTags->Add("axisvaluefont", fRT::TypeIDs::AXISVALUEFONT);
  pXlatTags->Add("background", fRT::TypeIDs::BACKGROUND);
  pXlatTags->Add("backgroundbrush", fRT::TypeIDs::BACKGROUNDBRUSH);
  pXlatTags->Add("backgroundcolor", fRT::TypeIDs::BACKGROUNDCOLOR);
  pXlatTags->Add("backgroundimage", fRT::TypeIDs::BACKGROUNDIMAGE);
  pXlatTags->Add("barborderbrush", fRT::TypeIDs::BARBORDERBRUSH);
  pXlatTags->Add("BarChart", fRT::TypeIDs::BARCHART);
  pXlatTags->Add("blurradius", fRT::TypeIDs::BLURRADIUS);
  pXlatTags->Add("border", fRT::TypeIDs::BORDER);
  pXlatTags->Add("borderbrush", fRT::TypeIDs::BORDERBRUSH);
  pXlatTags->Add("bordersize", fRT::TypeIDs::BORDERSIZE);
  pXlatTags->Add("borderthickness", fRT::TypeIDs::BORDERTHICKNESS);
  pXlatTags->Add("BoxAndWhiskerPlot", fRT::TypeIDs::BOXANDWHISKERPLOT);
  pXlatTags->Add("boxbrush", fRT::TypeIDs::BOXBRUSH);
  pXlatTags->Add("boxlinethickness", fRT::TypeIDs::BOXLINETHICKNESS);
  pXlatTags->Add("boxsize", fRT::TypeIDs::BOXSIZE);
  pXlatTags->Add("brush", fRT::TypeIDs::BRUSH);
  pXlatTags->Add("brushstate", fRT::TypeIDs::BRUSHSTATE);
  pXlatTags->Add("BrushState", fRT::TypeIDs::BRUSHSTATE);
  pXlatTags->Add("canadjustbeyondoperatingrange", fRT::TypeIDs::CANADJUSTBEYONDOPERATINGRANGE);
  pXlatTags->Add("canupdate", fRT::TypeIDs::CANUPDATE);
  pXlatTags->Add("CC_APP", fRT::TypeIDs::CCAPP);
  pXlatTags->Add("channel", fRT::TypeIDs::CHANNEL);
  pXlatTags->Add("channelname", fRT::TypeIDs::CHANNELNAME);
  pXlatTags->Add("channels", fRT::TypeIDs::CHANNELS);
  pXlatTags->Add("chart", fRT::TypeIDs::CHART);
  pXlatTags->Add("chartmode", fRT::TypeIDs::CHARTMODE);
  pXlatTags->Add("circularbarcolor", fRT::TypeIDs::CIRCULARBARCOLOR);
  pXlatTags->Add("circularbarthickness", fRT::TypeIDs::CIRCULARBARTHICKNESS);
  pXlatTags->Add("CircularGauge", fRT::TypeIDs::CIRCULARGAUGE);
  pXlatTags->Add("Client", fRT::TypeIDs::CLIENT);
  pXlatTags->Add("Client Address", fRT::TypeIDs::CLIENTADDRESS);
  pXlatTags->Add("colcount", fRT::TypeIDs::COLCOUNT);
  pXlatTags->Add("color1", fRT::TypeIDs::COLOR1);
  pXlatTags->Add("color2", fRT::TypeIDs::COLOR2);
  pXlatTags->Add("color2endoffset", fRT::TypeIDs::COLOR2ENDOFFSET);
  pXlatTags->Add("color3", fRT::TypeIDs::COLOR3);
  pXlatTags->Add("column", fRT::TypeIDs::COLUMN);
  pXlatTags->Add("columns", fRT::TypeIDs::COLUMNS);
  pXlatTags->Add("COMPUTER", fRT::TypeIDs::COMPUTER);
  pXlatTags->Add("connection", fRT::TypeIDs::CONNECTION);
  pXlatTags->Add("cornerradius", fRT::TypeIDs::CORNERRADIUS);
  pXlatTags->Add("countofaxisgridlines", fRT::TypeIDs::COUNTOFAXISGRIDLINES);
  pXlatTags->Add("countofgridlines", fRT::TypeIDs::COUNTOFGRIDLINES);
  pXlatTags->Add("countoftimegridlines", fRT::TypeIDs::COUNTOFTIMEGRIDLINES);
  pXlatTags->Add("cursor", fRT::TypeIDs::CURSOR);
  pXlatTags->Add("data", fRT::TypeIDs::DATA);
  pXlatTags->Add("DATABASE", fRT::TypeIDs::DATABASE);
  pXlatTags->Add("datamode", fRT::TypeIDs::DATAMODE);
  pXlatTags->Add("datapoint", fRT::TypeIDs::DATAPOINT);
  pXlatTags->Add("DataPoint", fRT::TypeIDs::DATAPOINT);
  pXlatTags->Add("datasource", fRT::TypeIDs::DATASOURCE);
  pXlatTags->Add("daybrush", fRT::TypeIDs::DAYBRUSH);
  pXlatTags->Add("db", fRT::TypeIDs::DB);
  pXlatTags->Add("default", fRT::TypeIDs::DEFAULT);
  pXlatTags->Add("DefaultDataSource", fRT::TypeIDs::DEFAULTDATASOURCE);
  pXlatTags->Add("delimiter", fRT::TypeIDs::DELIMITER);
  pXlatTags->Add("diameter", fRT::TypeIDs::DIAMETER);
  pXlatTags->Add("direction", fRT::TypeIDs::DIRECTION);
  pXlatTags->Add("distance", fRT::TypeIDs::DISTANCE);
  pXlatTags->Add("dropshadow", fRT::TypeIDs::DROPSHADOW);
  pXlatTags->Add("DynamicConnector", fRT::TypeIDs::DYNAMICCONNECTOR);
  pXlatTags->Add("DynamicImage", fRT::TypeIDs::DYNAMICIMAGE);
  pXlatTags->Add("DynamicText", fRT::TypeIDs::DYNAMICTEXT);
  pXlatTags->Add("DynamicTextAlarmedColor", fRT::TypeIDs::DYNAMICTEXTALARMEDCOLOR);
  pXlatTags->Add("DynamicTextDataSource", fRT::TypeIDs::DYNAMICTEXTDATASOURCE);
  pXlatTags->Add("DynamicTextFieldColor", fRT::TypeIDs::DYNAMICTEXTFIELDCOLOR);
  pXlatTags->Add("DynamicTextNotDefinedColor", fRT::TypeIDs::DYNAMICTEXTNOTDEFINEDCOLOR);
  pXlatTags->Add("DynamicTextSimulatedColor", fRT::TypeIDs::DYNAMICTEXTSIMULATEDCOLOR);
  pXlatTags->Add("endcolor", fRT::TypeIDs::ENDCOLOR);
  pXlatTags->Add("enddate", fRT::TypeIDs::ENDDATE);
  pXlatTags->Add("endendcap", fRT::TypeIDs::ENDENDCAP);
  pXlatTags->Add("End User", fRT::TypeIDs::ENDUSER);
  pXlatTags->Add("endvalue", fRT::TypeIDs::ENDVALUE);
  pXlatTags->Add("eu", fRT::TypeIDs::EU);
  pXlatTags->Add("excludemissing", fRT::TypeIDs::EXCLUDEMISSING);
  pXlatTags->Add("EU", fRT::TypeIDs::ENGINEERINGUNIT);
  pXlatTags->Add("exclusionbrush", fRT::TypeIDs::EXCLUSIONBRUSH);
  pXlatTags->Add("faceplatetickfont", fRT::TypeIDs::FACEPLATETICKFONT);
  pXlatTags->Add("faceplatetickforeground", fRT::TypeIDs::FACEPLATETICKFOREGROUND);
  pXlatTags->Add("faceplatetickvisibility", fRT::TypeIDs::FACEPLATETICKVISIBILITY);
  pXlatTags->Add("fillbrush", fRT::TypeIDs::FILLBRUSH);
  pXlatTags->Add("first", fRT::TypeIDs::FIRST);
  pXlatTags->Add("FixedPieChart", fRT::TypeIDs::FIXEDPIECHART);
  pXlatTags->Add("fixedtotal", fRT::TypeIDs::FIXEDTOTAL);
  pXlatTags->Add("flag", fRT::TypeIDs::FLAG);
  pXlatTags->Add("folder", fRT::TypeIDs::FOLDER);
  pXlatTags->Add("font", fRT::TypeIDs::FONT);
  pXlatTags->Add("fontsize", fRT::TypeIDs::FONTSIZE);
  pXlatTags->Add("fontstyle", fRT::TypeIDs::FONTSTYLE);
  pXlatTags->Add("foreground", fRT::TypeIDs::FOREGROUND);
  pXlatTags->Add("foregroundbrush", fRT::TypeIDs::FOREGROUNDBRUSH);
  pXlatTags->Add("format", fRT::TypeIDs::FORMAT);
  pXlatTags->Add("friendlyname", fRT::TypeIDs::FRIENDLYNAME);
  pXlatTags->Add("gauge", fRT::TypeIDs::GAUGE);
  pXlatTags->Add("glasseffect", fRT::TypeIDs::GLASSEFFECT);
  pXlatTags->Add("grid", fRT::TypeIDs::GRID);
  pXlatTags->Add("gridlinebrush", fRT::TypeIDs::GRIDLINEBRUSH);
  pXlatTags->Add("gridlines", fRT::TypeIDs::GRIDLINES);
  pXlatTags->Add("gridlinethickness", fRT::TypeIDs::GRIDLINETHICKNESS);
  pXlatTags->Add("headerfont", fRT::TypeIDs::HEADERFONT);
  pXlatTags->Add("height", fRT::TypeIDs::HEIGHT);
  pXlatTags->Add("Height", fRT::TypeIDs::HEIGHT);
  pXlatTags->Add("helplink", fRT::TypeIDs::HELPLINK);
  pXlatTags->Add("high", fRT::TypeIDs::HIGH);
  pXlatTags->Add("highbrush", fRT::TypeIDs::HIGHBRUSH);
  pXlatTags->Add("highbrushvisibility", fRT::TypeIDs::HIGHBRUSHVISIBILITY);
  pXlatTags->Add("highhigh", fRT::TypeIDs::HIGHHIGH);
  pXlatTags->Add("highhighbrush", fRT::TypeIDs::HIGHHIGHBRUSH);
  pXlatTags->Add("highhighbrushvisibility", fRT::TypeIDs::HIGHHIGHBRUSHVISIBILITY);
  pXlatTags->Add("highhighvalue", fRT::TypeIDs::HIGHHIGHVALUE);
  pXlatTags->Add("highvalue", fRT::TypeIDs::HIGHVALUE);
  pXlatTags->Add("historical", fRT::TypeIDs::HISTORICAL);
  pXlatTags->Add("hourbrush", fRT::TypeIDs::HOURBRUSH);
  pXlatTags->Add("HMI_ID", fRT::TypeIDs::HMIID);
  //pXlatTags->Add("object", fRT::TypeIDs::HMIOBJECT);
  pXlatTags->Add("id", fRT::TypeIDs::ID);
  pXlatTags->Add("image", fRT::TypeIDs::IMAGE);
  pXlatTags->Add("images", fRT::TypeIDs::IMAGES);
  pXlatTags->Add("imagestate", fRT::TypeIDs::IMAGESTATE);
  pXlatTags->Add("ImageState", fRT::TypeIDs::IMAGESTATE);
  pXlatTags->Add("isadjustable", fRT::TypeIDs::ISADJUSTABLE);
  pXlatTags->Add("isdashed", fRT::TypeIDs::ISDASHED);
  pXlatTags->Add("label", fRT::TypeIDs::LABEL);
  pXlatTags->Add("labelfont", fRT::TypeIDs::LABELFONT);
  pXlatTags->Add("labelforeground", fRT::TypeIDs::LABELFOREGROUND);
  pXlatTags->Add("labelforegroundbrush", fRT::TypeIDs::LABELFOREGROUNDBRUSH);
  pXlatTags->Add("labelvisibility", fRT::TypeIDs::LABELVISIBILITY);
  pXlatTags->Add("Launch", fRT::TypeIDs::LAUNCH);
  pXlatTags->Add("legend", fRT::TypeIDs::LEGEND);
  pXlatTags->Add("legendbackground", fRT::TypeIDs::LEGENDBACKGROUND);
  pXlatTags->Add("LIB", fRT::TypeIDs::LIB);
  pXlatTags->Add("LIB_CHANNEL", fRT::TypeIDs::LIBRARYCHANNEL);
  pXlatTags->Add("linethickness", fRT::TypeIDs::LINETHICKNESS);
  pXlatTags->Add("Last_Import", fRT::TypeIDs::LASTIMPORT);
  pXlatTags->Add("list", fRT::TypeIDs::LIST);
  pXlatTags->Add("listname", fRT::TypeIDs::LISTNAME);
  pXlatTags->Add("location", fRT::TypeIDs::LOCATION);
  pXlatTags->Add("logo", fRT::TypeIDs::LOGO);
  pXlatTags->Add("low", fRT::TypeIDs::LOW);
  pXlatTags->Add("lowbrush", fRT::TypeIDs::LOWBRUSH);
  pXlatTags->Add("lowbrushvisibility", fRT::TypeIDs::LOWBRUSHVISIBILITY);
  pXlatTags->Add("lowlow", fRT::TypeIDs::LOWLOW);
  pXlatTags->Add("lowlowbrush", fRT::TypeIDs::LOWLOWBRUSH);
  pXlatTags->Add("lowlowbrushvisibility", fRT::TypeIDs::LOWLOWBRUSHVISIBILITY);
  pXlatTags->Add("lowlowvalue", fRT::TypeIDs::LOWLOWVALUE);
  pXlatTags->Add("lowvalue", fRT::TypeIDs::LOWVALUE);
  pXlatTags->Add("maximum", fRT::TypeIDs::MAXIMUM);
  pXlatTags->Add("medianelipsestroke", fRT::TypeIDs::MEDIANELIPSESTROKE);
  pXlatTags->Add("menutext", fRT::TypeIDs::MENUTEXT);
  pXlatTags->Add("meter", fRT::TypeIDs::METER);
  pXlatTags->Add("Meter", fRT::TypeIDs::METER);
  pXlatTags->Add("METER", fRT::TypeIDs::METER);
  pXlatTags->Add("metermode", fRT::TypeIDs::METERMODE);
  pXlatTags->Add("minimum", fRT::TypeIDs::MINIMUM);
  pXlatTags->Add("name", fRT::TypeIDs::NAME);
  pXlatTags->Add("names", fRT::TypeIDs::NAMES);
  pXlatTags->Add("ndplot", fRT::TypeIDs::NDPLOT);
  pXlatTags->Add("needlelength", fRT::TypeIDs::NEEDLELENGTH);
  //pXlatTags->Add("nnn", fRT::TypeIDs::NETWORKADDRESS);
  pXlatTags->Add("NormalDistributionPlot", fRT::TypeIDs::NORMALDISTRIBUTIONPLOT);
  pXlatTags->Add("Note", fRT::TypeIDs::NOTE);
  pXlatTags->Add("notes", fRT::TypeIDs::NOTES);
  pXlatTags->Add("NOT_SVR", fRT::TypeIDs::NOTESERVER);
  pXlatTags->Add("numberformat", fRT::TypeIDs::NUMBERFORMAT);
  pXlatTags->Add("numberofmajorticks", fRT::TypeIDs::NUMBEROFMAJORTICKS);
  pXlatTags->Add("numberofminorticks", fRT::TypeIDs::NUMBEROFMINORTICKS);
  pXlatTags->Add("numberofpoints", fRT::TypeIDs::NUMBEROFPOINTS);
  pXlatTags->Add("ODBC_CONNECTION", fRT::TypeIDs::ODBCCONNECTION);
  pXlatTags->Add("ODBC_GROUP", fRT::TypeIDs::ODBCGROUP);
  pXlatTags->Add("ODBC_ITEM", fRT::TypeIDs::ODBCITEM);
  pXlatTags->Add("object", fRT::TypeIDs::OBJECT);
  pXlatTags->Add("objecttemplate", fRT::TypeIDs::OBJECTTEMPLATE);
  pXlatTags->Add("objecttemplatename", fRT::TypeIDs::OBJECTTEMPLATENAME);
  pXlatTags->Add("OIL_PROP", fRT::TypeIDs::OILPROP);
  pXlatTags->Add("opacity", fRT::TypeIDs::OPACITY);
  pXlatTags->Add("OPC_GROUP", fRT::TypeIDs::OPCGROUP);
  pXlatTags->Add("OPC_SERVER", fRT::TypeIDs::OPCSERVER);
  pXlatTags->Add("OPC_SIGNAL", fRT::TypeIDs::OPCSIGNAL);
  pXlatTags->Add("operatingrangefillbrush", fRT::TypeIDs::OPERATINGRANGEFILLBRUSH);
  pXlatTags->Add("operatingrangemaximum", fRT::TypeIDs::OPERATINGRANGEMAXIMUM);
  pXlatTags->Add("operatingrangeminimum", fRT::TypeIDs::OPERATINGRANGEMINIMUM);
  pXlatTags->Add("OPTIRAMP_OPC_SERVER", fRT::TypeIDs::OPTIRAMPOPCSERVER);
  pXlatTags->Add("orientation", fRT::TypeIDs::ORIENTATION);
  pXlatTags->Add("WEB_PAGE", fRT::TypeIDs::PAGE);
  pXlatTags->Add("page", fRT::TypeIDs::PAGE);
  pXlatTags->Add("pagestate", fRT::TypeIDs::PAGESTATE);
  pXlatTags->Add("PageState", fRT::TypeIDs::PAGESTATE);
  pXlatTags->Add("PANALOG", fRT::TypeIDs::PANALOG);
  pXlatTags->Add("PANALOG_OUT", fRT::TypeIDs::PANALOGOUT);
  pXlatTags->Add("password", fRT::TypeIDs::PASSWORD);
  pXlatTags->Add("PCORRECT", fRT::TypeIDs::PCORRECT);
  pXlatTags->Add("PDISCRETE", fRT::TypeIDs::PDISCRETE);
  pXlatTags->Add("PDISCRETE_OUT", fRT::TypeIDs::PDISCRETEOUT);
  pXlatTags->Add("permission", fRT::TypeIDs::PERMISSION);
  pXlatTags->Add("persist", fRT::TypeIDs::PERSIST);
  pXlatTags->Add("PieChart", fRT::TypeIDs::PIECHART);
  pXlatTags->Add("pivot", fRT::TypeIDs::PIVOT);
  pXlatTags->Add("plot", fRT::TypeIDs::PLOT);
  pXlatTags->Add("plotpointspathcolor", fRT::TypeIDs::PLOTPOINTSPATHCOLOR);
  pXlatTags->Add("plottype", fRT::TypeIDs::PLOTTYPE);
  pXlatTags->Add("plotweblinescolor", fRT::TypeIDs::PLOTWEBLINESCOLOR);
  pXlatTags->Add("point", fRT::TypeIDs::POINT);
  pXlatTags->Add("pointjoincap", fRT::TypeIDs::POINTJOINCAP);
  pXlatTags->Add("pointlabelformat", fRT::TypeIDs::POINTLABELFORMAT);
  pXlatTags->Add("pointscaletype", fRT::TypeIDs::POINTSCALETYPE);
  pXlatTags->Add("Popup", fRT::TypeIDs::POPUP);
  pXlatTags->Add("PopupLink", fRT::TypeIDs::POPUPLINK);
  pXlatTags->Add("popupname", fRT::TypeIDs::POPUPNAME);
  pXlatTags->Add("Port", fRT::TypeIDs::PORT);
  pXlatTags->Add("positionbackground", fRT::TypeIDs::POSITIONBACKGROUND);
  pXlatTags->Add("positiontext", fRT::TypeIDs::POSITIONTEXT);
  pXlatTags->Add("PNODE", fRT::TypeIDs::PNODE);
  pXlatTags->Add("predefinedrange", fRT::TypeIDs::PREDEFINEDRANGE);
  pXlatTags->Add("ProjectNotes", fRT::TypeIDs::PROJECTNOTES);
  pXlatTags->Add("projectnumber", fRT::TypeIDs::PROJECTNUMBER);
  pXlatTags->Add("Project Number", fRT::TypeIDs::PROJECTNUMBER);
  pXlatTags->Add("read-only-group", fRT::TypeIDs::READONLYGROUP);
  pXlatTags->Add("reference", fRT::TypeIDs::REFERENCE);
  pXlatTags->Add("ReferenceTable", fRT::TypeIDs::REFERENCETABLE);
  pXlatTags->Add("reftablecolumns", fRT::TypeIDs::REFTABLECOLUMNS);
  pXlatTags->Add("reftablerow", fRT::TypeIDs::REFTABLEROW);
  pXlatTags->Add("reftableunits", fRT::TypeIDs::REFTABLEUNITS);
  pXlatTags->Add("request", fRT::TypeIDs::REQUEST);
  pXlatTags->Add("REPORT_TREND_CHART_STYLE", fRT::TypeIDs::REPORTTRENDCHARTSTYLE);
  pXlatTags->Add("RPT_SVR", fRT::TypeIDs::REPORTSERVER);
  //pXlatTags->Add("nnn", fRT::TypeIDs::REVISION);
  pXlatTags->Add("ROOT", fRT::TypeIDs::ROOT);
  pXlatTags->Add("row", fRT::TypeIDs::ROW);
  pXlatTags->Add("rowcount", fRT::TypeIDs::ROWCOUNT);
  pXlatTags->Add("RuntimeImage", fRT::TypeIDs::RUNTIMEIMAGE);
  pXlatTags->Add("salt", fRT::TypeIDs::SALT);
  pXlatTags->Add("sampleend", fRT::TypeIDs::SAMPLEEND);
  pXlatTags->Add("samplestart", fRT::TypeIDs::SAMPLESTART);
  pXlatTags->Add("scale", fRT::TypeIDs::SCALE);
  pXlatTags->Add("scalefont", fRT::TypeIDs::SCALEFONT);
  pXlatTags->Add("scaletextcolor", fRT::TypeIDs::SCALETEXTCOLOR);
  pXlatTags->Add("scaletextsize", fRT::TypeIDs::SCALETEXTSIZE);
  pXlatTags->Add("scaletype", fRT::TypeIDs::SCALETYPE);
  pXlatTags->Add("scalevisibility", fRT::TypeIDs::SCALEVISIBILITY);
  pXlatTags->Add("security", fRT::TypeIDs::SECURITY);
  pXlatTags->Add("segment", fRT::TypeIDs::SEGMENT);
  pXlatTags->Add("setpoint", fRT::TypeIDs::SETPOINT);
  pXlatTags->Add("shadowdepth", fRT::TypeIDs::SHADOWDEPTH);
  pXlatTags->Add("shapetype", fRT::TypeIDs::SHAPETYPE);
  pXlatTags->Add("show", fRT::TypeIDs::SHOW);
  pXlatTags->Add("showaxisx", fRT::TypeIDs::SHOWAXISX);
  pXlatTags->Add("showaxisy", fRT::TypeIDs::SHOWAXISY);
  pXlatTags->Add("showbarvalues", fRT::TypeIDs::SHOWBARVALUES);
  pXlatTags->Add("showborder", fRT::TypeIDs::SHOWBORDER);
  pXlatTags->Add("showdatapoints", fRT::TypeIDs::SHOWDATAPOINTS);
  pXlatTags->Add("showdropshadow", fRT::TypeIDs::SHOWDROPSHADOW);
  pXlatTags->Add("showgridlines", fRT::TypeIDs::SHOWGRIDLINES);
  pXlatTags->Add("showhighhighvalue", fRT::TypeIDs::SHOWHIGHHIGHVALUE);
  pXlatTags->Add("showhighvalue", fRT::TypeIDs::SHOWHIGHVALUE);
  pXlatTags->Add("showlabel", fRT::TypeIDs::SHOWLABEL);
  pXlatTags->Add("showlegend", fRT::TypeIDs::SHOWLEGEND);
  pXlatTags->Add("showlegendsbar", fRT::TypeIDs::SHOWLEGENDSBAR);
  pXlatTags->Add("showlowlowvalue", fRT::TypeIDs::SHOWLOWLOWVALUE);
  pXlatTags->Add("showlowvalue", fRT::TypeIDs::SHOWLOWVALUE);
  pXlatTags->Add("showminorticks", fRT::TypeIDs::SHOWMINORTICKS);
  pXlatTags->Add("shownavigationbar", fRT::TypeIDs::SHOWNAVIGATIONBAR);
  pXlatTags->Add("showparametersbar", fRT::TypeIDs::SHOWPARAMETERSBAR);
  pXlatTags->Add("showpointlabels", fRT::TypeIDs::SHOWPOINTLABELS);
  pXlatTags->Add("showsetpoints", fRT::TypeIDs::SHOWSETPOINTS);
  pXlatTags->Add("showtitle", fRT::TypeIDs::SHOWTITLE);
  pXlatTags->Add("showtoolbar", fRT::TypeIDs::SHOWTOOLBAR);
  pXlatTags->Add("showvalue", fRT::TypeIDs::SHOWVALUE);
  pXlatTags->Add("showvalues", fRT::TypeIDs::SHOWVALUES);
  pXlatTags->Add("sid", fRT::TypeIDs::SID);
  pXlatTags->Add("sigma", fRT::TypeIDs::SIGMA);
  pXlatTags->Add("singleaxisvalueposition", fRT::TypeIDs::SINGLEAXISVALUEPOSITION);
  pXlatTags->Add("size", fRT::TypeIDs::SIZE);
  pXlatTags->Add("Solid", fRT::TypeIDs::SOLID);
  pXlatTags->Add("SpeedGauge", fRT::TypeIDs::SPEEDGAUGE);
  pXlatTags->Add("speedometerbackground", fRT::TypeIDs::SPEEDOMETERBACKGROUND);
  pXlatTags->Add("speedometerbordercolor", fRT::TypeIDs::SPEEDOMETERBORDERCOLOR);
  pXlatTags->Add("speedometerborderthickness", fRT::TypeIDs::SPEEDOMETERBORDERTHICKNESS);
  pXlatTags->Add("speedometerdiameter", fRT::TypeIDs::SPEEDOMETERDIAMETER);
  pXlatTags->Add("speedometervalueontop", fRT::TypeIDs::SPEEDOMETERVALUEONTOP);
  pXlatTags->Add("SpiderPlot", fRT::TypeIDs::SPIDERPLOT);
  pXlatTags->Add("startcolor", fRT::TypeIDs::STARTCOLOR);
  pXlatTags->Add("startdate", fRT::TypeIDs::STARTDATE);
  pXlatTags->Add("startendcap", fRT::TypeIDs::STARTENDCAP);
  pXlatTags->Add("startvalue", fRT::TypeIDs::STARTVALUE);
  pXlatTags->Add("StaticConnector", fRT::TypeIDs::STATICCONNECTOR);
  pXlatTags->Add("StaticImage", fRT::TypeIDs::STATICIMAGE);
  pXlatTags->Add("StaticLine", fRT::TypeIDs::STATICLINE);
  pXlatTags->Add("StaticShape", fRT::TypeIDs::STATICSHAPE);
  pXlatTags->Add("StaticText", fRT::TypeIDs::STATICTEXT);
  //pXlatTags->Add("nnn", fRT::TypeIDs::STATICTEXTWITHDYNAMICBACKGROUND);
  pXlatTags->Add("Table", fRT::TypeIDs::TABLE);
  pXlatTags->Add("table_channels", fRT::TypeIDs::TABLECHANNELS);
  pXlatTags->Add("tablemode", fRT::TypeIDs::TABLEMODE);
  pXlatTags->Add("table_values", fRT::TypeIDs::TABLEVALUES);
  pXlatTags->Add("target", fRT::TypeIDs::TARGET);
  pXlatTags->Add("targetvalue", fRT::TypeIDs::TARGETVALUE);
  pXlatTags->Add("targetvalues", fRT::TypeIDs::TARGETVALUES);
  pXlatTags->Add("templatename", fRT::TypeIDs::TEMPLATENAME);
  pXlatTags->Add("text", fRT::TypeIDs::TEXT);
  pXlatTags->Add("textbrush", fRT::TypeIDs::TEXTBRUSH);
  pXlatTags->Add("thickness", fRT::TypeIDs::THICKNESS);
  pXlatTags->Add("thumb", fRT::TypeIDs::THUMB);
  pXlatTags->Add("thumbborderbrush", fRT::TypeIDs::THUMBBORDERBRUSH);
  pXlatTags->Add("thumbborderthickness", fRT::TypeIDs::THUMBBORDERTHICKNESS);
  pXlatTags->Add("thumbfill", fRT::TypeIDs::THUMBFILL);
  pXlatTags->Add("tickbrush", fRT::TypeIDs::TICKBRUSH);
  pXlatTags->Add("tickthickness", fRT::TypeIDs::TICKTHICKNESS);
  pXlatTags->Add("timespan", fRT::TypeIDs::TIMESPAN);
  pXlatTags->Add("title", fRT::TypeIDs::TITLE);
  pXlatTags->Add("titlevisibility", fRT::TypeIDs::TITLEVISIBILITY);
  pXlatTags->Add("total", fRT::TypeIDs::TOTAL);
  pXlatTags->Add("trendchannel", fRT::TypeIDs::TRENDCHANNEL);
  pXlatTags->Add("TrendChart", fRT::TypeIDs::TRENDCHART);
  pXlatTags->Add("TREND_CHART_STYLE", fRT::TypeIDs::TRENDCHARTSTYLE);
  pXlatTags->Add("trendtemplatename", fRT::TypeIDs::TRENDTEMPLATENAME);
  pXlatTags->Add("type", fRT::TypeIDs::TYPE);
  pXlatTags->Add("uids", fRT::TypeIDs::UIDS);
  pXlatTags->Add("UID", fRT::TypeIDs::UID);
  pXlatTags->Add("unit", fRT::TypeIDs::UNIT);
  pXlatTags->Add("Unit", fRT::TypeIDs::UNIT);
  pXlatTags->Add("units", fRT::TypeIDs::UNITS);
  pXlatTags->Add("UpdatableText", fRT::TypeIDs::UPDATABLETEXT);
  pXlatTags->Add("updatecommandvalue", fRT::TypeIDs::UPDATECOMMANDVALUE);
  pXlatTags->Add("useactualdate", fRT::TypeIDs::USEACTUALDATE);
  pXlatTags->Add("user", fRT::TypeIDs::USER);
  pXlatTags->Add("username", fRT::TypeIDs::USERNAME);
  pXlatTags->Add("value", fRT::TypeIDs::VALUE);
  pXlatTags->Add("valuefont", fRT::TypeIDs::VALUEFONT);
  pXlatTags->Add("valueforegroundbrush", fRT::TypeIDs::VALUEFOREGROUNDBRUSH);
  pXlatTags->Add("valuelabelnumberformat", fRT::TypeIDs::VALUELABELNUMBERFORMAT);
  pXlatTags->Add("valuenumberformat", fRT::TypeIDs::VALUENUMBERFORMAT);
  pXlatTags->Add("valuesfont", fRT::TypeIDs::VALUESFONT);
  pXlatTags->Add("valuesforeground", fRT::TypeIDs::VALUESFOREGROUND);
  pXlatTags->Add("valuetype", fRT::TypeIDs::VALUETYPE);
  pXlatTags->Add("valuevisibility", fRT::TypeIDs::VALUEVISIBILITY);
  pXlatTags->Add("verticalbarbrush", fRT::TypeIDs::VERTICALBARBRUSH);
  pXlatTags->Add("verticalbarwidth", fRT::TypeIDs::VERTICALBARWIDTH);
  pXlatTags->Add("vts", fRT::TypeIDs::VTS);
  pXlatTags->Add("VTS_FOLDER", fRT::TypeIDs::VTSFOLDER);
  pXlatTags->Add("VTS_SERVER", fRT::TypeIDs::VTSSERVER);
  pXlatTags->Add("VTS_TAG", fRT::TypeIDs::VTSTAG);
  pXlatTags->Add("WEB_CONF", fRT::TypeIDs::WEBCONF);
  pXlatTags->Add("WEB_PICTS", fRT::TypeIDs::WEBIMAGES);
  pXlatTags->Add("WEB_LIBRARY", fRT::TypeIDs::WEBLIBRARY);
  pXlatTags->Add("WEB_PICTURE", fRT::TypeIDs::WEBPICTURE);
  pXlatTags->Add("WEB_STYLE", fRT::TypeIDs::WEBSTYLE);
  pXlatTags->Add("WEB_STYLES", fRT::TypeIDs::WEBSTYLES);
  pXlatTags->Add("WEB_SVR", fRT::TypeIDs::WEBSERVER);
  pXlatTags->Add("weekbrush", fRT::TypeIDs::WEEKBRUSH);
  pXlatTags->Add("width", fRT::TypeIDs::WIDTH);
  pXlatTags->Add("Width", fRT::TypeIDs::WIDTH);
  pXlatTags->Add("write-group", fRT::TypeIDs::WRITEGROUP);
  pXlatTags->Add("WIKI", fRT::TypeIDs::WIKI);
  pXlatTags->Add("x", fRT::TypeIDs::X);
  pXlatTags->Add("X", fRT::TypeIDs::X);
  pXlatTags->Add("y", fRT::TypeIDs::Y);
  pXlatTags->Add("Y", fRT::TypeIDs::Y);
 }

fElementType::fElementType(C_Project_Node_Type ^ pNode_Type)
{
  this->sTypeName = pNode_Type->sName;

  if (pXlatTags->ContainsKey(pNode_Type->sTag))
  {
    this->sTypeID = pXlatTags[pNode_Type->sTag];
  }
  else
  {
    this->sTypeID = pNode_Type->sTag;
  }
}

fElementType::fElementType(System::String ^ sType)
{
  this->sTypeName = sType;

  if (!pXlatTags->TryGetValue(sType, this->sTypeID))
  {
    this->sTypeID = sType;
  }
}

String ^ fElementType::Name::get()
{
  return sTypeName;
}

String ^ fElementType::TypeID::get()
{
  return sTypeID;
}

void fElementType::Add(fElementType ^ pElementType)
{
  for each (fElementType ^ pType in oElementTypes)
  {
    if (System::String::CompareOrdinal(pType->sTypeID, pElementType->sTypeID) == 0)
      return;
  }
  oElementTypes.Add(pElementType);
}

System::String ^ fElementType::XlatTypeID(System::String ^ sNodeTypeID)
{
  if (pXlatTags->ContainsKey(sNodeTypeID))
  {
    return pXlatTags[sNodeTypeID];
  }
  else
  {
#ifdef _DEBUG
    if (!pUnknowns->Contains(sNodeTypeID))
      pUnknowns->Add(sNodeTypeID);
#endif // _DEBUG
    return fRT::TypeIDs::UNKNOWN;
  }
}
