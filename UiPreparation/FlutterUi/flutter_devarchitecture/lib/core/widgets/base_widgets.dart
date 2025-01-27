import 'package:flutter/material.dart';
import '../theme/extensions.dart';
import '../theme/custom_colors.dart';

Widget buildInfoCardWithIcon({
  required BuildContext context,
  required IconData icon,
  double iconSize = 36,
  double headerSize = 16,
  double contentSize = 20,
  required Color color,
  required String title,
  required String value,
}) =>
    Container(
      decoration: BoxDecoration(
        border: Border.all(
            color: CustomColors.light.getColor,
            width: 1.5,
            style: BorderStyle.solid),
        boxShadow: [
          BoxShadow(
            color: CustomColors.light.getColor,
            blurRadius: 1,
          )
        ],
        borderRadius: context.lowestBorderRadius,
        color: CustomColors.white.getColor,
      ),
      alignment: Alignment.center,
      child: Padding(
        padding: context.lowestPadding,
        child: Row(
          children: [
            const Spacer(),
            Expanded(
              flex: 10,
              child: Column(
                mainAxisAlignment: MainAxisAlignment.start,
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  const Spacer(),
                  Expanded(
                    flex: 8,
                    child: Icon(
                      icon,
                      color: color,
                      size: iconSize,
                    ),
                  ),
                  const Spacer(),
                  Expanded(
                    flex: 8,
                    child: Text(
                      title,
                      style: TextStyle(
                          color: CustomColors.gray.getColor,
                          fontSize: headerSize,
                          fontWeight: FontWeight.w400,
                          fontFamily: "Helvetica"),
                    ),
                  ),
                  const Spacer(),
                  Expanded(
                      flex: 8,
                      child: Text(value,
                          style: TextStyle(
                            fontSize: contentSize,
                          ))),
                ],
              ),
            ),
          ],
        ),
      ),
    );

Widget buildInfoCardWithIconAndFooter(
  BuildContext context,
  IconData icon,
  String value,
  String title, {
  Color? color,
  String? footer,
}) =>
    Container(
      decoration: BoxDecoration(
        borderRadius: context.lowestBorderRadius,
        color: color ?? CustomColors.white.getColor,
      ),
      alignment: Alignment.center,
      child: Padding(
        padding: context.lowHorizontalPadding,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.start,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            const Spacer(),
            Expanded(
                flex: 80,
                child: Row(
                    mainAxisAlignment: MainAxisAlignment.start,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Expanded(
                          flex: 20,
                          child: Column(
                            children: [
                              const Spacer(),
                              Expanded(
                                  flex: 2,
                                  child: Row(
                                    crossAxisAlignment:
                                        CrossAxisAlignment.start,
                                    mainAxisAlignment: MainAxisAlignment.start,
                                    children: [
                                      Expanded(
                                          child: Text(title,
                                              style: TextStyle(
                                                fontWeight: FontWeight.w900,
                                                fontSize: 15,
                                                color:
                                                    CustomColors.white.getColor,
                                              )))
                                    ],
                                  )),
                              Expanded(
                                  flex: 4,
                                  child: Text(value,
                                      style: TextStyle(
                                        fontWeight: FontWeight.w900,
                                        color: CustomColors.white.getColor,
                                        fontSize: 40,
                                      ))),
                            ],
                          )),
                      Expanded(
                          flex: 10,
                          child: Column(
                            mainAxisAlignment: MainAxisAlignment.center,
                            crossAxisAlignment: CrossAxisAlignment.center,
                            children: [
                              Icon(
                                icon,
                                color: CustomColors.white.getColor,
                                size: 50,
                              )
                            ],
                          )),
                    ])),
            Expanded(
              child: Divider(
                  height: 0,
                  endIndent: 0,
                  indent: 0,
                  color: CustomColors.white.getColor),
            ),
            const Spacer(
              flex: 5,
            ),
            Expanded(
                flex: 20,
                child: Row(
                  children: [
                    const Spacer(),
                    Expanded(
                      flex: 12,
                      child: footer == null
                          ? const Spacer()
                          : Row(
                              children: [
                                footer.contains("-")
                                    ? Expanded(
                                        child: Icon(
                                        Icons.arrow_downward_rounded,
                                        color: CustomColors.white.getColor,
                                        size: 18,
                                      ))
                                    : footer.contains("+")
                                        ? Expanded(
                                            child: Icon(
                                            Icons.arrow_upward_rounded,
                                            color: CustomColors.white.getColor,
                                            size: 18,
                                          ))
                                        : const Spacer(),
                                Expanded(
                                  flex: 10,
                                  child: Text(footer,
                                      style: TextStyle(
                                          fontSize: 14,
                                          color: CustomColors.white.getColor)),
                                ),
                              ],
                            ),
                    )
                  ],
                )),
            const Spacer(),
          ],
        ),
      ),
    );

Widget buildPageTitle(BuildContext context, String pageTitle,
    {String? subDirection}) {
  subDirection = subDirection == null ? "" : "$subDirection > ";
  return Row(
      mainAxisAlignment: MainAxisAlignment.start,
      crossAxisAlignment: CrossAxisAlignment.center,
      children: [
        buildSubTitleWidget(context, "$subDirection$pageTitle"),
      ]);
}

Container buildCardWithTitle({
  required BuildContext context,
  required Widget body,
  required int bodyFlex,
  required double headerTitleSize,
  required String title,
  Widget? trailing,
}) {
  return Container(
    decoration: BoxDecoration(
      border: Border.all(
          color: CustomColors.light.getColor,
          width: 1.5,
          style: BorderStyle.solid),
      boxShadow: [
        BoxShadow(
          color: CustomColors.light.getColor,
          blurRadius: 1,
        )
      ],
      borderRadius: context.lowestBorderRadius,
      color: CustomColors.white.getColor,
    ),
    alignment: Alignment.center,
    child: Column(
      mainAxisAlignment: MainAxisAlignment.center,
      crossAxisAlignment: CrossAxisAlignment.center,
      children: [
        const Spacer(
          flex: 2,
        ),
        Expanded(
            flex: 7,
            child: Row(
                mainAxisAlignment: MainAxisAlignment.start,
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  const Spacer(
                    flex: 5,
                  ),
                  Expanded(
                    flex: 300,
                    child: Row(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      mainAxisAlignment: MainAxisAlignment.start,
                      children: [
                        const Spacer(),
                        Expanded(
                          flex: 8,
                          child: Text(
                            title.toUpperCase(),
                            style: TextStyle(
                              fontWeight: FontWeight.w500,
                              fontSize: headerTitleSize,
                              fontFamily: "Helvetica",
                            ),
                          ),
                        ),
                        const Spacer(),
                      ],
                    ),
                  ),
                  Expanded(flex: 5, child: trailing ?? Container()),
                ])),
        Expanded(flex: bodyFlex, child: body),
        const Spacer(),
      ],
    ),
  );
}

Container buildCardWithTitleAndSubTitle(
  BuildContext context,
  Widget body,
  int bodyFlex,
  int trailingFlex,
  double headerTitleSize,
  String title,
  String? subtitle,
  Widget? trailing,
) {
  return Container(
    decoration: BoxDecoration(
      border: Border.all(
          color: CustomColors.light.getColor,
          width: 1.5,
          style: BorderStyle.solid),
      boxShadow: [
        BoxShadow(
          color: CustomColors.light.getColor,
          blurRadius: 1,
        )
      ],
      borderRadius: context.lowestBorderRadius,
      color: CustomColors.white.getColor,
    ),
    alignment: Alignment.center,
    child: Column(
      mainAxisAlignment: MainAxisAlignment.center,
      crossAxisAlignment: CrossAxisAlignment.center,
      children: [
        const Spacer(),
        Expanded(
            flex: 5,
            child: Row(
                mainAxisAlignment: MainAxisAlignment.start,
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  const Spacer(
                    flex: 4,
                  ),
                  Expanded(
                    flex: 80,
                    child: Column(
                        mainAxisAlignment: MainAxisAlignment.start,
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Expanded(
                            flex: 40,
                            child: Text(
                              title,
                              style: TextStyle(
                                fontWeight: FontWeight.w900,
                                fontSize: headerTitleSize,
                              ),
                            ),
                          ),
                          Expanded(flex: 20, child: Text(subtitle ?? "")),
                          const Spacer(
                            flex: 5,
                          ),
                        ]),
                  ),
                  Expanded(flex: trailingFlex, child: trailing ?? Container()),
                  const Spacer(flex: 3),
                ])),
        Expanded(flex: bodyFlex, child: body),
        const Spacer(),
      ],
    ),
  );
}

Container buildInfoCardWithTitleAndFooter(BuildContext context, Widget body,
    int bodyFlex, double headerTitleSize, String title, String footerTitle) {
  return Container(
    decoration: BoxDecoration(
      border: Border.all(
          color: CustomColors.light.getColor,
          width: 1.5,
          style: BorderStyle.solid),
      boxShadow: [
        BoxShadow(
          color: CustomColors.light.getColor,
          blurRadius: 1,
        )
      ],
      borderRadius: context.lowestBorderRadius,
      color: CustomColors.white.getColor,
    ),
    alignment: Alignment.center,
    child: Padding(
      padding: context.lowLeftPadding,
      child: Column(
        mainAxisAlignment: MainAxisAlignment.start,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Spacer(),
          Expanded(
            flex: 2,
            child: Text(
              title,
              style: TextStyle(
                color: CustomColors.gray.getColor,
                fontWeight: FontWeight.w900,
                fontSize: headerTitleSize,
              ),
            ),
          ),
          Expanded(flex: bodyFlex, child: body),
          Expanded(
              flex: 2,
              child: Text(
                footerTitle,
                style: TextStyle(color: CustomColors.gray.getColor),
              )),
          const Spacer(),
        ],
      ),
    ),
  );
}

Container buildCardBase(
  BuildContext context,
  Widget child,
) {
  return Container(
    decoration: BoxDecoration(
      border: Border.all(
          color: CustomColors.light.getColor,
          width: 1.5,
          style: BorderStyle.solid),
      boxShadow: [
        BoxShadow(
          color: CustomColors.light.getColor,
          blurRadius: 1,
        )
      ],
      borderRadius: context.lowestBorderRadius,
      color: CustomColors.white.getColor,
    ),
    alignment: Alignment.center,
    child: Column(
      mainAxisAlignment: MainAxisAlignment.center,
      crossAxisAlignment: CrossAxisAlignment.center,
      children: [
        const Spacer(),
        Expanded(flex: 100, child: child),
        const Spacer(),
      ],
    ),
  );
}

Text buildTitleWidget(BuildContext context, String pageTitle) => Text(
      pageTitle,
      style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 28),
    );

Padding buildSubTitleWidget(BuildContext context, String pageTitle) => Padding(
      padding: context.lowHorizontalPadding,
      child: Text(
        pageTitle,
        style: const TextStyle(
            fontWeight: FontWeight.w500,
            fontSize: 20,
            fontStyle: FontStyle.italic),
      ),
    );

Text buildTableDefineElement(BuildContext context, String value) => Text(value,
    style: TextStyle(
      color: CustomColors.primary.getColor,
      fontWeight: FontWeight.w800,
      fontSize: 15,
    ));

Text buildTableElement(String value) => Text(value,
    style: TextStyle(
      color: CustomColors.white.getColor,
      fontWeight: FontWeight.w500,
      fontSize: 10,
    ));
