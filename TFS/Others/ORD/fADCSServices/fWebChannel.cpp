#include "fWebChannel.h"
#include "fProjectEUFactory.h"

// .Net namespace
using namespace System;

// Custom namespace
using namespace aCONF;
using namespace aWEB;
using namespace SNC::OptiRamp::Services::fDefs;
using namespace SNC::OptiRamp::Services::fWeb;
using namespace SNC::OptiRamp::Services::ProjectEUFactory;


fWebChannel::fWebChannel(C_Project_Node^ pNode_, array<C_Web_Channel> ^ pChannels_, int iIdx_)
  : pNode(pNode_),
    pChannels(pChannels_),
    iIdx(iIdx_)
{
}

String ^ fWebChannel::Description::get()
{
  return System::String::Empty;
}

void fWebChannel::Description::set(String^ descriptoin_)
{
  throw gcnew System::NotImplementedException;
}

String ^ fWebChannel::Name::get()
{
  return pChannels[iIdx].sName;
}

void fWebChannel::Name::set(String ^ name_)
{
  throw gcnew System::NotImplementedException;
}

String ^ fWebChannel::CustomerTag::get()
{
  return System::String::Empty;
}

void fWebChannel::CustomerTag::set(String^ customerTag_)
{
  throw gcnew System::NotImplementedException;
}

Nullable<double> fWebChannel::Min::get()
{
  return 0;
}

void fWebChannel::Min::set(Nullable<double> val)
{
  throw gcnew System::NotImplementedException;
}

Nullable<double> fWebChannel::Max::get()
{
  return 0;
}

void fWebChannel::Max::set(Nullable<double> val)
{
  throw gcnew System::NotImplementedException;
}

IProjectEU^ fWebChannel::EU::get()
{
  return fProjectEUFactory::CreateInstance(pChannels[iIdx].pConv);
}

void fWebChannel::EU::set(IProjectEU^ projectEU_)
{
  throw gcnew NotImplementedException;
}

bool fWebChannel::NCUType::get()
{
  return false;
}

void fWebChannel::NCUType::set(bool)
{
  throw gcnew NotImplementedException;
}

bool fWebChannel::DiscreteType::get()
{
  return false;
}

void fWebChannel::DiscreteType::set( bool )
{
  throw gcnew NotImplementedException;
}



Type ^ fWebChannel::Type::get()
{
  return pChannels[iIdx].pType;
}
