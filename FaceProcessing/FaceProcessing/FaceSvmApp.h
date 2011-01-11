// FaceSvm.h : main header file for the FaceSvm DLL
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CFaceSvmApp
// See FaceSvm.cpp for the implementation of this class
//

class CFaceSvmApp : public CWinApp
{
public:
	CFaceSvmApp();

// Overrides
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};
